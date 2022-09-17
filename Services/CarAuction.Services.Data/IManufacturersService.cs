using CarAuction.Data.Models.CarModel;

namespace CarAuction.Services.Data
{
    using System.Collections.Generic;

    public interface IManufacturersService
    {
        IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();

        Manufacturer GetById(int id);
    }
}
