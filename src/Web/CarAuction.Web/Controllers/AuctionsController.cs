namespace CarAuction.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels;
    using CarAuction.Web.ViewModels.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AuctionsController : Controller
    {
        private const int AuctionsPerPage = 24;
        private readonly IAuctionsService auctionsService;

        public AuctionsController(IAuctionsService auctionsService)
        {
            this.auctionsService = auctionsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
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
            return this.View(nameof(this.All));
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

            return this.View(viewModel);
        }

        public async Task<IActionResult> GetCarsByAuction(int id)
        {
            var viewModel = new RandomCarsViewModel();

            viewModel.Cars = await this.auctionsService.GetCarsByAuction<CarInListViewModel>(id);

            return this.View(viewModel);
        }
    }
}
