using System.Security.Claims;

namespace CarAuction.Web.Hubs
{
    using System.Threading.Tasks;

    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels.Bids;
    using Microsoft.AspNetCore.SignalR;

    public class CarBiddingHub : Hub
    {
        private readonly IBidsService bidsService;

        public CarBiddingHub(IBidsService bidsService)
        {
            this.bidsService = bidsService;
        }

        public async Task Send(decimal amountOfBid, int carId)
        {
            var user = this.Context.User;

            var model = await this.bidsService.MakeBid(amountOfBid, carId, user.FindFirstValue(ClaimTypes.NameIdentifier));

            var viewModel = new BidViewModel
            {
                UserName = user.Identity.Name,
                CreatedOn = model.CreatedOn.ToString("g"),
                AmountOfBid = model.AmountOfBid,
            };

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, carId.ToString());

            await this.Clients.Group(carId.ToString()).SendAsync("NewBid", viewModel);
        }
    }
}