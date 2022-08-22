using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data;
using CarAuction.Data.Models.AuctionModels;
using CarAuction.Data.Models.CarModel;
using CarAuction.Data.Models.CarModels;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext dbContext;

        public CarsService(ApplicationDbContext DbContext)
        {
            this.dbContext = DbContext;
        }

        public IEnumerable<ShowCarsViewModel> ShowAll()
        {
            var data = this.dbContext.Cars.Select(x => new ShowCarsViewModel()
            {
                Make = x.Model.Manufacturer.Name,
                Model = x.Model.Name,
                StartPrice = x.BuyNowPrice,
                DoorsCount = int.Parse(x.Model.CountOfDoors.ToString()),
                Transmission = x.Model.Engine.GearBox.ToString()
            })
                .ToList();

            return data;
        }
    }
}