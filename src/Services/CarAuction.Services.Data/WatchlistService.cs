namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    using static CarAuction.Common.GlobalConstants;

    public class WatchlistService : IWatchlistService
    {
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IDeletableEntityRepository<UserWatchedCars> userWatchedCarsRepository;

        public WatchlistService(
            IDeletableEntityRepository<Car> carsRepository,
            IDeletableEntityRepository<UserWatchedCars> userWatchedCarsRepository)
        {
            this.carsRepository = carsRepository;
            this.userWatchedCarsRepository = userWatchedCarsRepository;
        }

        public async Task AddAsync(int carId, string userId)
        {
            var car = await this.ReturnCarIfExistingAsync(carId);
            var userWatchedCarsEntity = await this.ReturnUserWatchlistAsync(userId);

            if (userWatchedCarsEntity.WatchedCars.Contains(car))
            {
               return;
            }

            userWatchedCarsEntity.WatchedCars.Add(car);

            await this.userWatchedCarsRepository.AddAsync(userWatchedCarsEntity);
            await this.userWatchedCarsRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int carId, string userId)
        {
            var car = await this.ReturnCarIfExistingAsync(carId);
            var user = await this.ReturnUserWatchlistAsync(userId);

            if (!user.WatchedCars.Contains(car))
            {
                throw new NullReferenceException(CarNotInUsersWatchlist);
            }

            user.WatchedCars.Remove(car);
            await this.userWatchedCarsRepository.SaveChangesAsync();
        }

        public async Task<ICollection<T>> ReturnAllWatchedByUserAsync<T>(string userId)
        {
            var user = await this.ReturnUserWatchlistAsync(userId);

            var watchedCars = await this.userWatchedCarsRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == user.UserId)
                .To<T>()
                .ToListAsync();

            if (watchedCars == null)
            {
                throw new ArgumentNullException(EmptyWatchedCarsCollection);
            }

            return watchedCars;
        }

        private async Task<UserWatchedCars> ReturnUserWatchlistAsync(string userId)
        {
            var user = await this.userWatchedCarsRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId) ?? new UserWatchedCars
            {
                UserId = userId,
            };

            return user;
        }

        private async Task<Car> ReturnCarIfExistingAsync(int carId)
        {
            var car = await this.carsRepository.All().FirstOrDefaultAsync(x => x.Id == carId);

            if (car == null)
            {
                throw new NullReferenceException(UnexistingListing);
            }

            return car;
        }
    }
}
