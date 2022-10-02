namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;
    using Microsoft.EntityFrameworkCore;

    public class ModelsService : IModelsService
    {
        private readonly IDeletableEntityRepository<Model> modelsRepository;

        public ModelsService(IDeletableEntityRepository<Model> modelsRepository)
        {
            this.modelsRepository = modelsRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs(int manufacturerId)
        {
            return this.modelsRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }

        public Model GetById(int id)
        {
            return this.modelsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Model> GetModels(int manufacturerId)
        {
            return this.modelsRepository
                .All()
                .Where(x => x.ManufacturerId == manufacturerId)
                .ToList();
        }
    }
}
