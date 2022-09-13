using Microsoft.AspNetCore.JsonPatch.Helpers;

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
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;
        private readonly IManufacturersService manufacturersService;
        private readonly IModelsService modelsService;
        private readonly IEnginesService enginesService;

        public CarsService(
            IDeletableEntityRepository<Manufacturer> manufacturersRepository,
            IManufacturersService manufacturersService,
            IModelsService modelsService,
            IEnginesService enginesService)
        {
            this.manufacturersRepository = manufacturersRepository;
            this.manufacturersService = manufacturersService;
            this.modelsService = modelsService;
            this.enginesService = enginesService;
        }

        public async Task CreateAsync(CarInputModel input, string userId, string imagePath)
        {
            var car = new Car()
            {
                ModelId = input.ModelId,
                StartingPrice = input.StartPrice,
                IsRunning = input.IsRunning,
                Milleage = input.Milleage,
                BuyNowPrice = input.BuyNowPrice,
                Color = input.ColorType
            };
            car.Model.Engine.TransmissionType = input.TransmissionType;

            if (car.Model.ManufacturerId != input.ManufacturerId)
            {
                throw new InvalidExpressionException("There is no Make/Model car");
            }
        }

        public Task<ICollection<CarInputModel>> ShowAll()
        {
            throw new NotImplementedException();
        }

        public CarInputModel PopulateDropdown(CarInputModel inputModel)
        {
            inputModel.Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs()
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            var manufacturer = this.manufacturersRepository.All().FirstOrDefault(x => x.Name == "BMW")!.Id;

            inputModel.Models = this.modelsService.GetAllAsKeyValuePairs(manufacturer)
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Engines = this.enginesService.GetAllAsKeyValuePairs(2)
                .Select(x => new SelectListItem(x.Value, x.Key.ToString()));

            inputModel.Transmissions = PopulateEnumValuesIntoDropdown<TransmissionType>();

            inputModel.Drivetrains = PopulateEnumValuesIntoDropdown<DrivetrainType>();

            inputModel.Fuels = PopulateEnumValuesIntoDropdown<FuelType>();

            inputModel.Colors = PopulateEnumValuesIntoDropdown<Color>();

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
