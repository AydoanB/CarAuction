namespace CarAuction.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        public IActionResult PageNotFound(int errorCode)
        {
            return this.View();
        }
    }
}
