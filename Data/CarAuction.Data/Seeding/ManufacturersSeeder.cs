using System;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;

namespace CarAuction.Data.Seeding
{
    public class ManufacturersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            var bmw = new Manufacturer()
            {
                Name = "BMW"
            };

            var audi = new Manufacturer()
            {
                Name = "AUDI"
            };
            var mb = new Manufacturer()
            {
                Name = "Mercedes-Benz"
            };
            await dbContext.Manufacturers.AddAsync(bmw);
            await dbContext.Manufacturers.AddAsync(audi);
            await dbContext.Manufacturers.AddAsync(mb);
        }
    }
}