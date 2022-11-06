namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWatchlistService
    {
         Task AddAsync(int carId, string userId);
         Task RemoveAsync(int carId, string userId);
         Task<IEnumerable<T>> ReturnAllWatchedByUserAsync<T>(string userId);
         Task<bool> IsInUsersWatchlist(string userId, int carId);
    }
}
