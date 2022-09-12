namespace CarAuction.Services.Data
{
    using System.Collections.Generic;

    public interface IManufacturersService
    {
        IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs();
    }
}
