using System;
using CarAuction.Services.Data;

namespace CarAuction.Web.Controllers
{
    using System.Threading.Tasks;

    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AuctionsController : Controller
    {
        private readonly IAuctionService auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            this.auctionService = auctionService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuctionInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.auctionService.CreateAsync(inputModel, Guid.NewGuid().ToString(), "");
            return this.View("/");
        }
    }
}