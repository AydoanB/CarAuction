using System;
using System.Linq;
using System.Net;
using System.Threading;
using AngleSharp;
using CarAuction.Data;
using CarAuction.Data.Common.Repositories;
using CarAuction.Data.Models.CarModel;
using CarAuction.Services.Data.JsonImport;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AutoDataScraper : IAutoDataScraper
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturerRepository;
        private readonly IDeletableEntityRepository<Model> modelRepository;
        private IConfiguration config;
        private readonly IBrowsingContext context;

        public AutoDataScraper(
           IDeletableEntityRepository<Manufacturer> manufacturerRepository,
           IDeletableEntityRepository<Model> modelRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.modelRepository = modelRepository;
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
        }

        public async Task PopulateDbWithData()
        {
            var makeModels = new ConcurrentBag<CarsImportDto>();

            Parallel.For(1, 1000, (i) =>
            {
                try
                {
                    Thread.Sleep(2 * 60 * 60);

                    var makeModel = this.GetMakeModels(i);

                    if(makeModel.Make != null && makeModel.Models.Any())
                    {
                        makeModels.Add(makeModel);
                    }

                    Thread.Sleep(2 * 60 * 60);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });

            foreach (var dto in makeModels)
            {
                Manufacturer manufacturer = this.manufacturerRepository.AllAsNoTracking().FirstOrDefault(x => x.Name.ToLower() == dto.Make.ToLower());
                if (manufacturer == null)
                {
                    manufacturer = new Manufacturer()
                    {
                        Name = dto.Make,
                    };
                }

                foreach (var modelName in dto.Models.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    if (manufacturer.Models.Any(x => x.Name.ToLower() == modelName.ToLower()) == false)
                    {
                        var model = new Model()
                        {
                            Name = modelName,
                        };
                        await modelRepository.AddAsync(model);
                        await modelRepository.SaveChangesAsync();

                        manufacturer.Models.Add(model);
                    }
                }

                await manufacturerRepository.AddAsync(manufacturer);
                await this.manufacturerRepository.SaveChangesAsync();
            }
        }

        private CarsImportDto GetMakeModels(int id)
        {
            var importModel = new CarsImportDto();
            try
            {
                var document = context.OpenAsync($"https://www.auto-data.net/bg/brand-{id}")
                    .GetAwaiter()
                    .GetResult();

                var unexstingMessage = document.QuerySelector("h1").TextContent;
                if (document.StatusCode == HttpStatusCode.NotFound || unexstingMessage == "Страницата не е намерена!") ;

                var divElement = document.QuerySelector("body").QuerySelector(".textinsite");

                var make = divElement.QuerySelector("strong").TextContent;
                Console.WriteLine($"Make: {make}");
                importModel.Make = make;

                var element = document.QuerySelector(".modelite");

                var listItems = element.QuerySelectorAll("li");

                foreach (var listItem in listItems)
                {
                    var text = listItem.QuerySelector("ul");

                    var lis = text.QuerySelectorAll("li");

                    foreach (var li in lis)
                    {
                        var modelName = li.QuerySelector("a > strong").TextContent;
                        importModel.Models.Add(modelName);

                        Console.WriteLine($"Model: {modelName}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return importModel;
        }
    }
}
