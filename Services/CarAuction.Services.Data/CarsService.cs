namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Data.Models.Enums;
    using CarAuction.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarsService : ICarsService
    {
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IManufacturersService manufacturersService;
        private readonly IModelsService modelsService;
        private readonly IEnginesService enginesService;
        private readonly IAuctionService auctionService;

        public CarsService(
            IDeletableEntityRepository<Car> carsRepository,
            IManufacturersService manufacturersService,
            IModelsService modelsService,
            IEnginesService enginesService,
            IAuctionService auctionService)
        {
            this.carsRepository = carsRepository;
            this.manufacturersService = manufacturersService;
            this.modelsService = modelsService;
            this.enginesService = enginesService;
            this.auctionService = auctionService;
        }

        public async Task CreateAsync(CarInputModel input, string userId, string imagePath)
        {
            var model = this.modelsService.GetById(input.ModelId);

            model.Manufacturer = this.manufacturersService.GetById(input.ManufacturerId);
            model.Engine = this.enginesService.GetById(input.EngineId);
            model.Engine.TransmissionType = input.TransmissionType;
            model.Engine.FuelType = input.FuelType;
            model.Engine.HorsePower = input.HorsePower;
            model.Drivetrain = input.DrivetrainType;
            model.VehicleType = input.VehicleType;

            var car = new Car()
            {
                Model = model,
                StartingPrice = input.StartPrice,
                IsRunning = input.IsRunning,
                Milleage = input.Milleage,
                BuyNowPrice = input.BuyNowPrice,
                Color = input.ColorType,
                Auction = this.auctionService.GetById(input.AuctionId)
            };

            if (car.Model.ManufacturerId != input.ManufacturerId)
            {
                throw new InvalidExpressionException("There is no Make/Model car");
            }

            await this.carsRepository.AddAsync(car);
            await this.carsRepository.SaveChangesAsync();
        }

        public Task<ICollection<CarInputModel>> ShowAll()
        {
            throw new NotImplementedException();
        }

        public CarInputModel PopulateDropdowns(CarInputModel inputModel)
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
