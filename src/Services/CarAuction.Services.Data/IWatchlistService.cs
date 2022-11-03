namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWatchlistService
    {
         Task AddAsync(int carId, string userId);
         Task RemoveAsync(int carId, string userId);

         Task<ICollection<T>> ReturnAllWatchedByUserAsync<T>(string userId);
    }
}
