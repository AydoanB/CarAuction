using System.Collections.Generic;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services.Cars
{
    public interface ICarsService
    {
        IEnumerable<ShowCarsViewModel> ShowAll();
        Task AddCar(string modelName, string color, int mileage);
    }
}