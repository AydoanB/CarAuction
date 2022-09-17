using System.Threading.Tasks;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services.Data
{
    using System.Collections.Generic;

    using CarAuction.Data.Models.AuctionModels;

    public interface IAuctionService
    {
        Task CreateAsync(AuctionInputModel input, string userId, string imagePath);
        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();
        Auction GetById(int id);
    }
}