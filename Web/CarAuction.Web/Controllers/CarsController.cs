using CarAuction.Services.Cars;
using CarAuction.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService CarsService)
        {
            this.carsService = CarsService;
        }

        public IActionResult All()
        {
            var carsViewModels = carsService.ShowAll();

            return View(carsViewModels);
        }
    }
}
