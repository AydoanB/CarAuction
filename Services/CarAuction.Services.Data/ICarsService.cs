namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuction.Web.ViewModels;

    public interface ICarsService
    {
        Task CreateAsync(CarInputModel input, string userId, string imagePath);
        Task<ICollection<CarInputModel>> ShowAll();

        public CarInputModel PopulateDropdowns(CarInputModel inputModel);
    }
}
