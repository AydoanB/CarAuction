namespace CarAuction.Services.Data
{
    using System.Collections.Generic;

    using CarAuction.Data.Models.AuctionModels;

    public interface IAuctionService
    {
        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();
        Auction GetById(int id);
    }
}