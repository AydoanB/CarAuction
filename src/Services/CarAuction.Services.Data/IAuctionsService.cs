namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Web.ViewModels;

    public interface IAuctionsService
    {
        Task CreateAsync(AuctionInputModel input, string userId);
        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();
        IEnumerable<T> GetAllAuctions<T>(int page, int auctionsPerPage, string order, out int auctionsCount);
        Auction GetById(int id);
        Task<IEnumerable<T>> GetCarsByAuction<T>(int id);
    }
}
