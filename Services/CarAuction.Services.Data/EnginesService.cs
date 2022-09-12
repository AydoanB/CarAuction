namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.CarModel;

    public class EnginesService : IEnginesService
    {
        private readonly IDeletableEntityRepository<Engine> engines;

        public EnginesService(IDeletableEntityRepository<Engine> engines)
        {
            this.engines = engines;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs(int model)
        {
            return this.engines.AllAsNoTracking().Select(x => new
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
