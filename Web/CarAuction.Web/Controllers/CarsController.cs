namespace CarAuction.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;

        public CarsController(
            ICarsService carsService)
        {
            this.carsService = carsService;
        }

        [HttpGet]
        public Task<IActionResult> Add()
        {
            var viewModel = new CarInputModel();

            viewModel = this.carsService.PopulateDropdown(viewModel);

            return Task.FromResult<IActionResult>(this.View(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarInputModel car)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(car);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var cars = await this.carsService.ShowAll();

            return this.View(cars.FirstOrDefault());
        }
    }
}
