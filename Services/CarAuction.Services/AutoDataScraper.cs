namespace CarAuction.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using AngleSharp;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Services.Data.JsonImport;
    using Newtonsoft.Json;
    using RestSharp;

    public class AutoDataScraper : IAutoDataScraper
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration appConfiguration;
        private readonly IDeletableEntityRepository<Model> _modelsRepo;
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        public AutoDataScraper(
            IDeletableEntityRepository<Manufacturer> manufacturersRepository,
            Microsoft.Extensions.Configuration.IConfiguration appConfiguration)
        {
            this.manufacturersRepository = manufacturersRepository;
            this.appConfiguration = appConfiguration;
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
        }

        public async Task PopulateDbWithDataWithScraping()
        {
            var makeModels = new ConcurrentBag<CarsImportDto>();

            Parallel.For(1, 1000, (i) =>
            {
                try
                {
                    Thread.Sleep(2 * 60 * 60);

                    var makeModel = this.GetMakeModels(i);

                    if (makeModel.Make != null && makeModel.Models.Any())
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
                Manufacturer manufacturer = this.manufacturersRepository.AllAsNoTracking()
                    .FirstOrDefault(x => x.Name.ToLower() == dto.Make.ToLower());
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

                        manufacturer.Models.Add(model);
                    }
                }

                await manufacturersRepository.AddAsync(manufacturer);
                await this.manufacturersRepository.SaveChangesAsync();
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

        public async Task PopulateDbWithDataFromApi(int limit)
        {
            var dbManufacturer = this.manufacturersRepository.All().ToList();

            foreach (var manufacturer in dbManufacturer)
            {
                var client = new RestClient($"https://api.api-ninjas.com/v1/cars?limit={limit}&make={manufacturer.Name}");

                var request = new RestRequest();
                request.AddHeader("X-Api-Key", appConfiguration.GetSection("ApiKey").Value);

                var response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    continue;
                }

                var makeModelPair = JsonConvert.DeserializeObject<IList<ApiNinjasCarImportModel>>(response.Content);
                IList<Model> models = new List<Model>();
                foreach (var dto in makeModelPair)
                {
                    Console.WriteLine(dto.Make);
                    Console.WriteLine(dto.Model[0].ToString().ToUpper() + dto.Model.Substring(1));

                    var model = new Model()
                    {
                        Name = dto.Model[0].ToString().ToUpper() + dto.Model.Substring(1),
                    };
                    models.Add(model);
                }

                manufacturer.Models = models
                    .DistinctBy(x=>x.Name)
                    .ToList();

                await manufacturersRepository.SaveChangesAsync();
            }
        }
    }
}
