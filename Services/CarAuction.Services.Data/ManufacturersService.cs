namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;

    public class ManufacturersesService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;

        public ManufacturersesService(IDeletableEntityRepository<Manufacturer> manufacturersRepository)
        {
            this.manufacturersRepository = manufacturersRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs()
        {
           return this.manufacturersRepository.All().Distinct()
               .Select(x => new
            {
                x.Id,
                x.Name,
            })
               .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }

        public Manufacturer GetById(int id)
        {
            return this.manufacturersRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
