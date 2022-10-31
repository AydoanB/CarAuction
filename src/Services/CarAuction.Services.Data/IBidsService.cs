namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.AuctionModels;

    public interface IBidsService
    {
        Task<Bid> MakeBid(decimal amountOfBid, int carId, string userId);
        Task DeleteAllRelatedBids(int carId);
    }
}
