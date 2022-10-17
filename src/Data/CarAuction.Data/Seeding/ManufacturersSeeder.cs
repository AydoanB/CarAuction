namespace CarAuction.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModel;

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
                Name = "BMW",
            };

            var audi = new Manufacturer()
            {
                Name = "AUDI",
            };
            var mb = new Manufacturer()
            {
                Name = "Mercedes-Benz",
            };
            await dbContext.Manufacturers.AddAsync(bmw);
            await dbContext.Manufacturers.AddAsync(audi);
            await dbContext.Manufacturers.AddAsync(mb);
            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Opel"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Renault"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Alfa-Romeo"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Fiat"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Ford"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Honda"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Kia"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Maserati"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Lamborghini"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Mazda"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Nissan"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Subaru"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Skoda"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Volkswagen"
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = "Volvo"
            });
        }
    }
}