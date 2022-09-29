using CarAuction.Data.Models.Enums;

namespace CarAuction.Web.ViewModels;

using System.Linq;
using AutoMapper;
using CarAuction.Common;
using CarAuction.Data.Models.CarModel;
using CarAuction.Services.Mapping;

public class SingleCarViewModel : IMapFrom<Car>, IHaveCustomMappings
{
    public int Id { get; set; }

    public string ModelManufacturerName { get; set; }

    public string ModelName { get; set; }

    public string ImageUrl { get; set; }

    public long Milleage { get; set; }

    public VehicleType ModelVehicleType { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal BuyNowPrice { get; set; }

    public int ModelYearOfProduction { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Car, SingleCarViewModel>()
            .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x =>
                    $"/images/{GlobalConstants.CarsImagesFolder}/" + x.Images.FirstOrDefault().Id + "." +
                    x.Images.FirstOrDefault().Extension));
    }
}