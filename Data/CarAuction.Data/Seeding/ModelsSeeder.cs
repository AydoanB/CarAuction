using System;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;

namespace CarAuction.Data.Seeding
{
    public class ModelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Models.Any())
            {
                return;
            }

            await dbContext.Models.AddAsync(new Model()
            {
                EngineId = 1,
                Drivetrain = "Rear-Wheel drive",
                ManufacturerId = 1,
                Name = "E92",
                YearOfProduction = 2008,
                VehicleType = "Coupe"
            });
        }
    }
}