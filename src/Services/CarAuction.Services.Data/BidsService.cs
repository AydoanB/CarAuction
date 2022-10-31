namespace CarAuction.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.AuctionModels;
    using Microsoft.EntityFrameworkCore;

    using static CarAuction.Common.GlobalConstants;

    public class BidsService : IBidsService
    {
        private readonly IDeletableEntityRepository<Bid> bidsRepository;
        private readonly ApplicationDbContext db;

        public BidsService(
            IDeletableEntityRepository<Bid> bidsRepository,
            ApplicationDbContext db)
        {
            this.bidsRepository = bidsRepository;
            this.db = db;
        }

        public Task<Bid> MakeBid(decimal amountOfBid, int carId, string userId)
        {
            // SignalR problem with async methods
            this.UpdateCarPrice(amountOfBid, carId);

            var bid = new Bid
            {
                CarId = carId,
                UserId = userId,
                AmountOfBid = amountOfBid,
                IsBuyNow = false,
            };
            this.db.Bids.Add(bid);
            this.db.SaveChanges();

            return Task.FromResult(bid);
        }

        public async Task DeleteAllRelatedBids(int carId)
        {
            var queryable = this.bidsRepository
                .All()
                .Where(x => x.CarId == carId);

            await queryable
                .ForEachAsync(x => this.bidsRepository.Delete(x));

            await this.bidsRepository.SaveChangesAsync();
        }

        private void UpdateCarPrice(decimal bidAmount, int carId)
        {
            var car = this.db
                .Cars
                .FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                throw new NullReferenceException(UnexistingListing);
            }

            if (car.CurrentPrice >= bidAmount)
            {
                throw new InvalidOperationException(string.Format(InvalidBid, car.CurrentPrice));
            }

            car.CurrentPrice = bidAmount;
            this.db.SaveChanges();
        }
    }
}
