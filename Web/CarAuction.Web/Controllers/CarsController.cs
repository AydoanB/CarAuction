using CarAuction.Services.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService CarsService)
        {
            carsService = CarsService;
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult AddCar()
        {
            return this.View();
        }
    }
}
