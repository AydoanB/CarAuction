﻿namespace CarAuction.Web
{
    using System.Reflection;

    using CarAuction.Data;
    using CarAuction.Data.Common;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models;
    using CarAuction.Data.Repositories;
    using CarAuction.Data.Seeding;
    using CarAuction.Services;
    using CarAuction.Services.Data;
    using CarAuction.Services.Mapping;
    using CarAuction.Services.Messaging;
    using CarAuction.Web.Hubs;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Stripe;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                    options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddRazorRuntimeCompilation();

            services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
            });
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();

            services.AddAntiforgery(opt =>
            {
                opt.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IModelsService, ModelsService>();
            services.AddTransient<IManufacturersService, ManufacturersesService>();
            services.AddTransient<IAuctionsService, AuctionsService>();
            services.AddTransient<IEnginesService, EnginesService>();
            services.AddTransient<ICarsService, CarsService>();
            services.AddTransient<IBidsService, BidsService>();
            services.AddTransient<IWatchlistService, WatchlistService>();

            services.AddTransient<IAutoDataScraper, AutoDataScraper>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter()
                    .GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            StripeConfiguration.ApiKey = "sk_test_51LzOm1FJJMRKuV7qZH1eDFQUmLbGaApzqf7zjvZueylZN6YwOTAozLqWWqLLfZ3srGO0eb15sUGjTzUVcNSqhLQp00AmzzvukX";
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseStatusCodePagesWithRedirects("/Error/PageNotFound?errorCode=404");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapHub<CarBiddingHub>("/bid");
            app.MapRazorPages();
        }
    }
}
