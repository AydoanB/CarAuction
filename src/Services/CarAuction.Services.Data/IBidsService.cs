namespace CarAuction.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBidsService
    {
        Task MakeBid(decimal amountOfBid, int carId, string userId);
        Task<ICollection<T>> GetAllBidsForCarAsync<T>(int carId);
    }
}
