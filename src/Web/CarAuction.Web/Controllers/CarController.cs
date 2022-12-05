namespace CarAuction.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarAuction.Common;
    using CarAuction.Services;
    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using CarAuction.Web.ViewModels.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private const int CarsPerPage = 12;

        private readonly ICarsService carsService;
        private readonly IAutoDataScraper autoDataScraper;
        private readonly IWebHostEnvironment environment;
        private readonly IModelsService modelsService;
        private readonly IWatchlistService watchlistService;

        public CarsController(
            ICarsService carsService,
            IAutoDataScraper autoDataScraper,
            IWebHostEnvironment environment,
            IModelsService modelsService,
            IWatchlistService watchlistService)
        {
            this.carsService = carsService;
            this.autoDataScraper = autoDataScraper;
            this.environment = environment;
            this.modelsService = modelsService;
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        [Authorize]
        public Task<IActionResult> Add()
        {
            var viewModel = new CarInputModel();

            viewModel = this.carsService.PopulateDropdowns(viewModel);

            return Task.FromResult<IActionResult>(this.View(viewModel));
        }

        [HttpPost]
        [Authorize]
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

            return this.RedirectToAction(nameof(this.All));
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
                ItemsPerPage = CarsPerPage,
                PageNumber = id,
                Cars = this.carsService.GetCarsToSearch<CarInListViewModel>(id, CarsPerPage, searchModel, order, out carsCount),
                ItemsCount = carsCount,
                Order = order,
            };

            if (id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            viewModel.SearchModel = this.carsService.PopulateSearchInputModelDropdowns(searchModel);

            return this.View(viewModel);
        }

        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> ById(int id)
        {
            string userId = null;

            var model = await this.carsService.GetCarByIdAsync<SingleCarViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (userId == model.UserId)
            {
                model.IsAbleToEditAndDelete = true;
            }

            model.IsInUsersWatchlist = await this.watchlistService.IsInUsersWatchlist(userId, id);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var input = await this.carsService.GetCarByIdAsync<EditCarInputModel>(id);
            if (input == null)
            {
                return this.NotFound();
            }

            if (input.UserId != this.User.FindFirst(ClaimTypes.NameIdentifier).Value || !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            input = this.carsService.PopulateDropdowns(input);
            return this.View(input);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditCarInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input = this.carsService.PopulateDropdowns(input);
                return this.View(input);
            }

            await this.carsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.carsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(All));
        }

        public Task<JsonResult> ModelsById(int manufacturerId)
        {
            var models = this.modelsService.GetModels(manufacturerId);

            return Task.FromResult(this.Json(models));
        }

        public async Task<IActionResult> AddToWatchlist(int id)
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            try
            {
                await this.watchlistService.AddAsync(id, userId);
            }
            catch (NullReferenceException nullReffExcp)
            {
                this.ModelState.AddModelError(string.Empty, nullReffExcp.Message);
                return this.NotFound(this.ModelState);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public async Task<IActionResult> RemoveFromWatchlist(int id)
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            try
            {
                await this.watchlistService.RemoveAsync(id, userId);
            }
            catch (NullReferenceException nullReffExcp)
            {
                this.ModelState.AddModelError(string.Empty, nullReffExcp.Message);
                return this.NotFound(this.ModelState);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public async Task<IActionResult> WatchedCars()
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            try
            {
                var viewModel = new RandomCarsViewModel()
                {
                    Cars = await this.watchlistService.ReturnAllWatchedByUserAsync<CarInListViewModel>(userId),
                };

                return this.View(viewModel);
            }
            catch (ArgumentNullException argNullExc)
            {
                this.ModelState.AddModelError(string.Empty, argNullExc.Message);
                return this.RedirectToAction(nameof(this.All));
            }
        }

        public async Task<IActionResult> Scrape()
        {
            await this.autoDataScraper.PopulateDbWithDataWithScraping();

            return this.Redirect(nameof(this.Add));
        }

        public async Task<IActionResult> ApiData([FromQuery] int limit)
        {
            await this.autoDataScraper.PopulateDbWithDataFromApi(limit);

            return this.Redirect(nameof(this.Add));
        }
    }
}
