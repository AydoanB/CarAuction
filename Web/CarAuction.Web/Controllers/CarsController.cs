using System;
using System.Security.Claims;
using CarAuction.Services;
using Microsoft.AspNetCore.Hosting;

namespace CarAuction.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private const int CarsPerPage = 12;

        private readonly ICarsService carsService;
        private readonly IAutoDataScraper autoDataScraper;
        private readonly IWebHostEnvironment environment;

        public CarsController(
            ICarsService carsService,
            IAutoDataScraper autoDataScraper,
            IWebHostEnvironment environment)
        {
            this.carsService = carsService;
            this.autoDataScraper = autoDataScraper;
            this.environment = environment;
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

            return this.Redirect("/index");
        }

        public async Task<IActionResult> All(string order, SearchCarInputModel searchModel, int id = 1)
        {
            int carsCount;

            var viewModel = new CarsListViewModel()
            {
                CarsPerPage = CarsPerPage,
                PageNumber = id,
                Cars = this.carsService
                    .GetAllForListingsPage<
                        CarInListViewModel>(), //(id, CarsPerPage, searchModel, order, out carsCount),
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

        public async Task<IActionResult> Scrape()
        {
            await this.autoDataScraper.PopulateDbWithData();

            return this.View();
        }
    }
}
