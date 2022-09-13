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
                Color = input.Color
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

            inputModel.Transmissions = Enum.GetValues<TransmissionType>().Select(x => new
            {
                Value = x.ToString(),
                Text = x.ToString(),
            }).ToList().Select(x => new SelectListItem(x.Value, x.Text));

            inputModel.Drivetrains = Enum.GetValues<DrivetrainType>().Select(x => new
            {
                Value = x.ToString(),
                Text = x.ToString(),
            }).ToList().Select(x => new SelectListItem(x.Value, x.Text));

            inputModel.Fuels = Enum.GetValues<FuelType>().Select(x => new
            {
                Value = x.ToString(),
                Text = x.ToString(),
            }).ToList().Select(x => new SelectListItem(x.Value, x.Text));

            return inputModel;
        }

        // private static SelectListItem PopulateEnumValuesIntoDropdown<TEnum>(string type) where TEnum : struct, IConvertible
        // {
        //     return Enum.GetValues(typeof(TEnum)).Select(x => new
        //     {
        //         Value = x.ToString(),
        //         Text = x.ToString()
        //     }).ToList().Select(x => new SelectListItem(x.Value, x.Text));
        // }
    }
}
