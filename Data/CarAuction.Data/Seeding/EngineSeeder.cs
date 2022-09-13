namespace CarAuction.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModel;
    using CarAuction.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

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
                HorsePower = 6,
                FuelType = FuelType.Diesel,
                TransmissionType = TransmissionType.Automatic,
            });
        }
    }
}
