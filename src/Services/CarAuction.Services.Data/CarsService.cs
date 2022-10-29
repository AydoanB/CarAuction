using CarAuction.Data;
using static CarAuction.Common.GlobalConstants;

namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Data.Models.CarModels;
    using CarAuction.Data.Models.Enums;
    using CarAuction.Services.Mapping;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CarsService : ICarsService
    {
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IManufacturersService manufacturersService;
        private readonly IModelsService modelsService;
        private readonly IEnginesService enginesService;
        private readonly IAuctionsService auctionsService;
        private readonly IImagesService imagesService;
        private readonly ApplicationDbContext db;

        public CarsService(
            IDeletableEntityRepository<Car> carsRepository,
            IManufacturersService manufacturersService,
            IModelsService modelsService,
            IEnginesService enginesService,
            IAuctionsService auctionsService,
            IImagesService imagesService,
            ApplicationDbContext db)
        {
            this.carsRepository = carsRepository;
            this.manufacturersService = manufacturersService;
            this.modelsService = modelsService;
            this.enginesService = enginesService;
            this.auctionsService = auctionsService;
            this.imagesService = imagesService;
            this.db = db;
        }

        public async Task CreateAsync(CarInputModel input, string userId, string imagePath)
        {
            var model = this.modelsService.GetById(input.ModelModelId);

            Engine engine = this.enginesService.GetById(input.EngineId) == null
                ? new Engine()
                : this.enginesService.GetById(input.EngineId);

            model.Manufacturer = this.manufacturersService.GetById(input.ModelManufacturerId);

            engine.Name = "No Engine specified";
            model.Engine = engine;
            model.Engine.TransmissionType = input.ModelEngineTransmissionType;
            model.Engine.FuelType = input.ModelEngineFuelType;
            model.Engine.HorsePower = input.ModelEngineHorsePower;

            model.Drivetrain = input.ModelDrivetrainType;
            model.VehicleType = input.ModelVehicleType;
            model.YearOfProduction = input.ModelYearOfProduction;

            var car = new Car()
            {
                Model = model,
                StartingPrice = input.StartPrice,
                IsRunning = input.IsRunning,
                Milleage = input.Milleage,
                BuyNowPrice = input.BuyNowPrice,
                Color = input.Color,
                Auction = this.auctionsService.GetById(input.AuctionId),
                UserId = userId
            };

            Directory.CreateDirectory($"{imagePath}/{CarsImagesFolder}/");

            foreach (var image in input.Images)
            {
                Image carImage = this.imagesService.CreateImage(image, userId);

                car.Images.Add(carImage);

                await this.imagesService
                    .SaveImageToWebRootAsync(imagePath, carImage, image, CarsImagesFolder);
            }

            await this.carsRepository.AddAsync(car);
            await this.carsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await this.carsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.carsRepository.Delete(car);

            await this.carsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditCarInputModel input)
        {
            var car = await this.carsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            var model = this.modelsService.GetById(input.ModelModelId);

            Engine engine = this.enginesService.GetById(input.EngineId) == null
                ? new Engine()
                : this.enginesService.GetById(input.EngineId);

            model.Manufacturer = this.manufacturersService.GetById(input.ModelManufacturerId);

            model.Engine = engine;
            model.Engine.TransmissionType = input.ModelEngineTransmissionType;
            model.Engine.FuelType = input.ModelEngineFuelType;
            model.Engine.HorsePower = input.ModelEngineHorsePower;

            model.Drivetrain = input.ModelDrivetrainType;
            model.VehicleType = input.ModelVehicleType;

            car.Model = model;

            carsRepository.Update(car);
            await carsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetCarsToSearch<T>(int page, int carsPerPage, SearchCarInputModel searchModel, string order, out int carsCount)
        {
            var query = this.carsRepository.All().AsQueryable();

            if (searchModel?.Search != null)
            {
                query = query.Where(x =>
                    x.Model.Name.Contains(searchModel.Search) ||
                    x.Model.Manufacturer.Name.Contains(searchModel.Search));
            }

            if (searchModel?.ManufacturerId != 0)
            {
                    query = query.Where(x => x.Model.ManufacturerId == searchModel.ManufacturerId);
            }

            if (searchModel?.ModelId != 0)
            {
                    query = query.Where(x => x.Model.ManufacturerId == searchModel.ModelId);
            }

            if (searchModel?.StartPriceFrom != null && searchModel?.StartPriceTo != null)
            {
                query = query.Where(x =>
                        x.StartingPrice >= searchModel.StartPriceFrom && x.StartingPrice <= searchModel.StartPriceTo);
            }

            carsCount = query.Count();

            if (order != null)
            {
                if (order == "A-Z")
                {
                    query = query.OrderBy(x => $"{x.Model.Name}{x.Model.Manufacturer.Name}");
                }
                else if (order == "Z-A")
                {
                    query = query.OrderByDescending(x => $"{x.Model.Name}{x.Model.Manufacturer.Name}");
                }
                else if (order == "Price-descending")
                {
                    query = query.OrderByDescending(x => x.StartingPrice);
                }
                else if (order == "Price-ascending")
                {
                    query = query.OrderBy(x => x.StartingPrice);
                }
            }

            return query
                .Skip((page - 1) * carsPerPage)
                .Take(carsPerPage)
                .To<T>()
                .ToList();
        }

        public T PopulateDropdowns<T>(T inputModel)
        where T : CarInputModelDropdownItems
        {
            inputModel.Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Models = this.modelsService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Engines = this.enginesService.GetAllAsKeyValuePairs(2)
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Auctions = this.auctionsService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Transmissions = PopulateEnumValuesIntoDropdown<TransmissionType>();

            inputModel.Drivetrains = PopulateEnumValuesIntoDropdown<DrivetrainType>();

            inputModel.Fuels = PopulateEnumValuesIntoDropdown<FuelType>();

            inputModel.Colors = PopulateEnumValuesIntoDropdown<Color>();

            inputModel.Vehicles = PopulateEnumValuesIntoDropdown<VehicleType>();

            return inputModel;
        }

        public async Task<T> GetCarByIdAsync<T>(int id)
        {
            return await this.carsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRandomCars<T>(int count)
        {
            return await this.carsRepository
                .AllAsNoTracking()
                .To<T>()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }

        private static List<SelectListItem> PopulateEnumValuesIntoDropdown<T>()
        {
            Type enumType = typeof(T);

            var enumList = new List<SelectListItem>();

            foreach (var enumValue in Enum.GetValues(enumType))
            {
                enumList.Add(new SelectListItem()
                {
                    Text = enumValue.ToString(),
                    Value = enumValue.ToString(),
                });
            }

            return enumList;
        }

        public SearchCarInputModel PopulateSearchInputModelDropdowns(SearchCarInputModel viewModel)
        {
            viewModel.ManufacturerItems = this.manufacturersService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            viewModel.ModelsItems = this.modelsService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            viewModel.AuctionItems = this.auctionsService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            return viewModel;
        }

        public void UpdateCarPrice(decimal bidAmount, int carId)
        {
           var car = this.db
               .Cars
               .FirstOrDefault(x => x.Id == carId);

           if (car == null)
           {
               throw new ArgumentNullException(UnexistingListing);
           }

           if (car.CurrentPrice >= bidAmount)
           {
               throw new InvalidOperationException(string.Format(InvalidBid, car.CurrentPrice));
           }

           car.CurrentPrice = bidAmount;
           this.db.SaveChanges();
        }
    }
}
