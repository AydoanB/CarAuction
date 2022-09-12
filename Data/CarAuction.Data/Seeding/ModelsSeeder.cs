namespace CarAuction.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModel;
    using CarAuction.Data.Models.Enums;

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
                Drivetrain = DrivetrainType.rwd,
                ManufacturerId = 1,
                Name = "E92",
                YearOfProduction = 2008,
                VehicleType = VehicleType.Coupe,
            });
        }
    }
}