namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;

    public class ManufacturersesService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturers;

        public ManufacturersesService(IDeletableEntityRepository<Manufacturer> manufacturers)
        {
            this.manufacturers = manufacturers;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs()
        {
           return this.manufacturers.All().Distinct()
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
            return this.manufacturers.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
