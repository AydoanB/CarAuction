using System.Linq;

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
            var userId = this.Context.UserIdentifier;

            await this.bidsService.MakeBid(amountOfBid, carId, userId);

            await this.Clients.All.SendAsync("NewBid", amountOfBid);
        }

        public async Task Receive(int carId)
        {
            var bidsForCar = await this.bidsService.GetAllBidsForCarAsync<BidViewModel>(carId);

            await this.Clients.All.SendAsync("GetAll", bidsForCar);
        }
    }
}
