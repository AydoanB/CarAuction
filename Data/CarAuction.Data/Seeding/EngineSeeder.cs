using System;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;
using CarAuction.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Data.Seeding
{
    public class EngineSeeder : ISeeder

    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Engines.Any())
            {
                return;
            }

            await dbContext.Engines.AddAsync(new Engine()
            {
                Name = "335D",
                Capacity = 3500,
                Cylinders = 6,
                FuelType = "Diesel",
                GearBox = GearBox.Automatic,
                
            });
        }
    }
}