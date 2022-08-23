using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data;
using CarAuction.Data.Models.AuctionModels;
using CarAuction.Data.Models.CarModel;
using CarAuction.Data.Models.CarModels;
using CarAuction.Services.Data;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly CarsApiService carsApi;

        public CarsService(ApplicationDbContext DbContext, CarsApiService CarsApi)
        {
            this.dbContext = DbContext;
            this.carsApi = CarsApi;
        }

        public IEnumerable<ShowCarsViewModel> ShowAll()
        {
            var result = this.carsApi.AddCars("audi", 5);
            return new List<ShowCarsViewModel>();
        }
    }
}