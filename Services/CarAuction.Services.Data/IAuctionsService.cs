namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Web.ViewModels;

    public interface IAuctionsService
    {
        Task CreateAsync(AuctionInputModel input, string userId, string imagePath);
        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();
        Auction GetById(int id);
    }
}