using System;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Data.Models.AuctionModels;

namespace CarAuction.Data.Seeding
{
    public class AuctionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Auctions.Any())
            {
                return;
            }

            await dbContext.Auctions.AddAsync(new Auction()
            {
                Location = new Location()
                {
                    Name = "Sofia",
                },
                StartDate = DateTime.UtcNow.Date,
            });
        }
    }
}
