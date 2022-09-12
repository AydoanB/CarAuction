namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarAuction.Web.ViewModels;

    public interface ICarsService
    {
        Task CreateAsync(CarInputModel car);
        Task<ICollection<CarInputModel>> ShowAll();

        CarInputModel PopulateDropdown(CarInputModel inputModel);
    }
}
