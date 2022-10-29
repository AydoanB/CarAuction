using System;
using CarAuction.Common;
using CarAuction.Data.Models.CarModel;

namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class BidsService : IBidsService
    {
        private readonly IDeletableEntityRepository<Bid> bidsRepository;
        private readonly ICarsService carsService;
        private readonly ApplicationDbContext db;

        public BidsService(IDeletableEntityRepository<Bid> bidsRepository,
            ICarsService carsService,
            ApplicationDbContext db)
        {
            this.bidsRepository = bidsRepository;
            this.carsService = carsService;
            this.db = db;
        }

        public async Task MakeBid(decimal amountOfBid, int carId, string userId)
        {
            this.carsService.UpdateCarPrice(amountOfBid, carId);

            var bid = new Bid
            {
                CarId = carId,
                UserId = userId,
                AmountOfBid = amountOfBid,
                IsBuyNow = false,
            };
            this.db.Bids.Add(bid);
        }

        public async Task<ICollection<T>> GetAllBidsForCarAsync<T>(int carId)
        {
            return await this.bidsRepository
                .All()
                .Where(x => x.CarId == carId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(5)
                .To<T>()
                .ToListAsync();
        }
    }
}
