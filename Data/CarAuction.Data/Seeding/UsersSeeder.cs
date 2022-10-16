using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CarAuction.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            await dbContext.Users.AddAsync(new ApplicationUser()
            {
               Email = "dummy@mail.com",
               PasswordHash = "dummy",
               UserName = "dummy",
            });
        }
    }
}
