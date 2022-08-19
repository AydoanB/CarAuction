using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data;
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
                StartPrice = x.BuyNowPrice
            }).ToList();

            return data;
        }
        public async Task AddCar(string modelName, string color, int mileage)
        {
            var modelId = dbContext.Models.FirstOrDefault(x => x.Name == modelName).Id;

            var car = new Car()
            {
                Accessories = new List<Accessory>(),
                AuctionId = 4,
                Color = color,
                IsRunning = true,
                Milleage = mileage,
                ModelId = modelId
            };

            dbContext.Cars.Add(car);
            dbContext.SaveChangesAsync();
        }
    }
}