using CarAuction.Data.Models.Enums;

namespace CarAuction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;
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

        public async Task CreateAsync(CarInputModel car)
        {

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
                Text = x.ToString()
            }).ToList().Select(x => new SelectListItem(x.Value, x.Text));

            return inputModel;
        }
    }
}
