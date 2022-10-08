using AutoMapper;

namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Common;
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
        private readonly IAuctionService auctionService;
        private readonly IImageService imagesService;

        public CarsService(
            IDeletableEntityRepository<Car> carsRepository,
            IManufacturersService manufacturersService,
            IModelsService modelsService,
            IEnginesService enginesService,
            IAuctionService auctionService,
            IImageService imagesService)
        {
            this.carsRepository = carsRepository;
            this.manufacturersService = manufacturersService;
            this.modelsService = modelsService;
            this.enginesService = enginesService;
            this.auctionService = auctionService;
            this.imagesService = imagesService;
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
                Auction = this.auctionService.GetById(input.AuctionId),
                UserId = userId
            };

            Directory.CreateDirectory($"{imagePath}/{GlobalConstants.CarsImagesFolder}/");

            foreach (var image in input.Images)
            {
                Image carImage = this.imagesService.CreateImage(image, userId);

                car.Images.Add(carImage);

                await this.imagesService
                    .SaveImageToWebRootAsync(imagePath, carImage, image, GlobalConstants.CarsImagesFolder);
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

            if (searchModel?.MakesIds != null)
            {
                foreach (var manufacturerId in searchModel.MakesIds)
                {
                    query = query.Where(x => x.Model.ManufacturerId == manufacturerId);
                }
            }

            if (searchModel?.ModelsIds != null)
            {
                foreach (var manufacturerId in searchModel.ModelsIds)
                {
                    query = query.Where(x => x.Model.ManufacturerId == manufacturerId);
                }
            }

            if (searchModel?.VehicleTypes != null)
            {
                foreach (var vehicleType in searchModel.VehicleTypes)
                {
                    query = query.Where(x => x.Model.VehicleType == vehicleType);
                }
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

            inputModel.Models = this.modelsService.GetAllAsKeyValuePairs(1)
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Engines = this.enginesService.GetAllAsKeyValuePairs(2)
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Auctions = this.auctionService.GetAllAsKeyValuePairs()
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
    }
}
