namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuction.Web.ViewModels;

    public interface ICarsService
    {
        Task CreateAsync(CarInputModel input, string userId, string imagePath);
        IEnumerable<T> GetAllForListingsPage<T>();
        CarInputModel PopulateDropdowns(CarInputModel inputModel);

        IEnumerable<T> GetCarsToSearch<T>(int page, int carsPerPage, SearchCarInputModel searchModel, string order, out int carsCount);
    }
}
