using CarAuction.Data.Models.CarModel;
using Newtonsoft.Json;

namespace CarAuction.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarAuction.Services;
    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private const int CarsPerPage = 12;

        private readonly ICarsService carsService;
        private readonly IAutoDataScraper autoDataScraper;
        private readonly IWebHostEnvironment environment;
        private readonly IModelsService modelsService;

        public CarsController(
            ICarsService carsService,
            IAutoDataScraper autoDataScraper,
            IWebHostEnvironment environment,
            IModelsService modelsService)
        {
            this.carsService = carsService;
            this.autoDataScraper = autoDataScraper;
            this.environment = environment;
            this.modelsService = modelsService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public Task<JsonResult> ModelsById(int manufacturerId)
        {
            var models = this.modelsService.GetModels(manufacturerId);

            return Task.FromResult(this.Json(models));
        }

        [HttpGet]
        public Task<IActionResult> Add()
        {
            var viewModel = new CarInputModel();

            viewModel = this.carsService.PopulateDropdowns(viewModel);

            return Task.FromResult<IActionResult>(this.View(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarInputModel input)
        {
            string userId = null;
            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.carsService.CreateAsync(input, userId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(String.Empty, e.Message);
                input = this.carsService.PopulateDropdowns(input);

                return this.View(input);
            }

            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All(string order, SearchCarInputModel searchModel, int id = 1)
        {
            int carsCount;

            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new CarsListViewModel()
            {
                CarsPerPage = CarsPerPage,
                PageNumber = id,
                Cars = this.carsService.GetCarsToSearch<CarInListViewModel>(id, CarsPerPage, searchModel, order, out carsCount),
                CarsCount = 2,
                Order = order,
            };

            if (id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            //viewModel.SearchModel = this.carsService.PopulateDropdowns();

            return this.View(viewModel);
        }

        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> ById(int id)
        {
            var model = await this.carsService.GetCarByIdAsync<SingleCarViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            string userId = null;
            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return this.View(model);
        }

        public async Task<IActionResult> Scrape()
        {
            await this.autoDataScraper.PopulateDbWithData();

            return this.View();
        }
    }
}
