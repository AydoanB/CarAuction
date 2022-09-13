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
        private readonly IDeletableEntityRepository<Model> models;

        public ModelsService(IDeletableEntityRepository<Model> models)
        {
            this.models = models;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs(int manufacturerId)
        {
            return this.models.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }
    }
}