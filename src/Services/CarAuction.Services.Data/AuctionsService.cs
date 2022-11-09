namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Services.Mapping;

    using CarAuction.Web.ViewModels;

    public class AuctionsService : IAuctionsService
    {
        private readonly IDeletableEntityRepository<Auction> auctionRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;

        public AuctionsService(
            IDeletableEntityRepository<Auction> auctionRepository,
            IDeletableEntityRepository<Location> locationRepository)
        {
            this.auctionRepository = auctionRepository;
            this.locationRepository = locationRepository;
        }

        public async Task CreateAsync(AuctionInputModel input, string userId)
        {
            var location = this.locationRepository
                .All()
                .FirstOrDefault(x => x.Name.ToLower() == input.Location.Name.ToLower());

            if (location != null)
            {
                throw new Exception("Location exists");
            }

            var auction = new Auction()
            {
                Location = new Location()
                {
                    Name = input.Location.Name,
                },
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                UserId = userId,
            };

            await this.auctionRepository.AddAsync(auction);
            await this.auctionRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs()
        {
            return this.auctionRepository.AllAsNoTracking().Select(x => new
                {
                    x.Id,
                    x.Location.Name,
                })
                .OrderBy(x => x.Name)
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }

        public IEnumerable<T> GetAllAuctions<T>(int page, int auctionsPerPage, string order, out int auctionsCount)
        {
            var query = this.auctionRepository
                .AllAsNoTracking()
                .AsQueryable();

            auctionsCount = query.Count();

            return query
                .Skip((page - 1) * auctionsPerPage)
                .Take(auctionsPerPage)
                .To<T>()
                .ToList();
        }

        public Auction GetById(int id)
        {
            return this.auctionRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetCarsByAuction<T>(int id)
        {
            return this.auctionRepository
                .AllAsNoTracking()
                .AsQueryable()
                .Where(x => x.Id == id)
                .Select(x => x.CarsInAuction)
                .To<T>();
        }
    }
}
