namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModel;

    public interface IModelsService
    {
        IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs(int manufacturerId);

        Model GetById(int id);
    }
}
