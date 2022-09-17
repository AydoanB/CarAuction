using System.Collections.Generic;
using System.Linq;
using CarAuction.Data.Common.Repositories;
using CarAuction.Data.Models.AuctionModels;

namespace CarAuction.Services.Data
{
    public class AuctionService : IAuctionService
    {
        private readonly IDeletableEntityRepository<Auction> auctionRepository;

        public AuctionService(IDeletableEntityRepository<Auction> auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs()
        {
            return this.auctionRepository.AllAsNoTracking().Select(x => new
                {
                    x.Id,
                    x.Location.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }

        public Auction GetById(int id)
        {
            return this.auctionRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
