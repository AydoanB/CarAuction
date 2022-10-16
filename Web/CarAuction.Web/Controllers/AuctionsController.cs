using System;
using System.Security.Claims;
using CarAuction.Services.Data;

namespace CarAuction.Web.Controllers
{
    using System.Threading.Tasks;

    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AuctionsController : Controller
    {
        private readonly IAuctionsService auctionsService;
        private const int AuctionsPerPage = 24;

        public AuctionsController(IAuctionsService auctionsService)
        {
            this.auctionsService = auctionsService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuctionInputModel inputModel)
        {
            string userId = null;
            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.auctionsService.CreateAsync(inputModel, userId);
            return this.View(nameof(All));
        }

        public async Task<IActionResult> All(string order, SearchCarInputModel searchModel, int id = 1)
        {
            int auctionsCount;

            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new AuctionListViewModel()
            {
                ItemsPerPage = AuctionsPerPage,
                PageNumber = id,
                Auctions = this.auctionsService.GetAllAuctions<AuctionInListViewModel>(id, AuctionsPerPage, order, out auctionsCount),
                ItemsCount = auctionsCount,
                Order = order,
            };

            if (id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            //viewModel.SearchModel = this.carsService.PopulateDropdowns();

            return this.View(viewModel);
        }
    }
}