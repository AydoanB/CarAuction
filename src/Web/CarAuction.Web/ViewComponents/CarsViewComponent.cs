using CarAuction.Web.ViewModels.Cars;

namespace CarAuction.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CarsViewComponent : ViewComponent
    {
        private readonly ICarsService carsService;

        public CarsViewComponent(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new RandomCarsViewModel();
            viewModel.Cars = await this.carsService.GetRandomCars<CarInListViewModel>(10);

            return this.View("FeaturedCars", viewModel);
        }
    }
}
