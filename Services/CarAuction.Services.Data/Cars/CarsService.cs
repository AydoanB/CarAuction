using System;
using System.Collections.Generic;
using CarAuction.Data;
using CarAuction.Web.ViewModels;
using CarAuction.Services;


namespace CarAuction.Services.Data.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext dbContext;
        //private readonly CarsApiService carsApi;

        public CarsService(ApplicationDbContext DbContext)
        {
            this.dbContext = DbContext;
            //this.carsApi = CarsApi;
        }

        public IEnumerable<ShowCarsViewModel> ShowAll()
        {
            //var result = this.carsApi.AddCars("audi", 5);
            return new List<ShowCarsViewModel>();
        }
    }
}


