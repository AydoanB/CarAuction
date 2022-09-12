namespace CarAuction.Services.Data
{
    using System.Collections.Generic;

    public interface IEnginesService
    {
        IEnumerable<KeyValuePair<int, string>> GetAllAsKeyValuePairs(int model);
    }
}
