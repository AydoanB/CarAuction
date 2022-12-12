namespace CarAuction.Web.Hubs
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarAuction.Services.Data;
    using CarAuction.Web.ViewModels.Bids;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
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
            try
            {
                var model = await this.bidsService.MakeBid(amountOfBid, carId, user.FindFirst(ClaimTypes.NameIdentifier).Value);

                var viewModel = new BidViewModel
                {
                    UserName = user.Identity.Name,
                    CreatedOn = model.CreatedOn.ToString("g"),
                    AmountOfBid = model.AmountOfBid,
                };

                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, carId.ToString());

                await this.Clients.Group(carId.ToString()).SendAsync("NewBid", viewModel);
            }
            catch (InvalidOperationException invalidOpsExc)
            {
                throw new HubException(invalidOpsExc.Message.Split(":")[0]);
            }
        }
    }
}
