using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Web.Controllers
{
    public class CarController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
