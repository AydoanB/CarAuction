namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuction.Web.ViewModels;

    public interface ICarsService
    {
        Task CreateAsync(CarInputModel input, string userId, string imagePath);
        IEnumerable<T> GetAllForListingsPage<T>();
        T PopulateDropdowns<T>(T inputModel)
            where T : CarInputModel;

        IEnumerable<T> GetCarsToSearch<T>(int page, int carsPerPage, SearchCarInputModel searchModel, string order, out int carsCount);
        Task<T> GetCarByIdAsync<T>(int id);

    }
}
