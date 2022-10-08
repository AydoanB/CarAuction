using System.Collections.Generic;
using CarAuction.Data.Models.Enums;
using CarAuction.Web.ViewModels.Images;

namespace CarAuction.Web.ViewModels;

using System.Linq;
using AutoMapper;
using CarAuction.Common;
using CarAuction.Data.Models.CarModel;
using CarAuction.Services.Mapping;

public class SingleCarViewModel : IMapFrom<Car>, IHaveCustomMappings
{
    public string UserId { get; set; }

    public bool IsAbleToEditAndDelete { get; set; }
    public int Id { get; set; }

    public string ModelManufacturerName { get; set; }

    public string ModelName { get; set; }

    public string ImageUrl { get; set; }

    public long Milleage { get; set; }

    public VehicleType ModelVehicleType { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal BuyNowPrice { get; set; }

    public int ModelYearOfProduction { get; set; }

    public TransmissionType ModelEngineTransmissionType { get; set; }

    public int ModelEngineHorsePower { get; set; }

    public FuelType ModelEngineFuelType { get; set; }

    public DrivetrainType ModelDrivetrain { get; set; }

    public string Color { get; set; }

    public IEnumerable<CarImageViewModel> Images { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Car, SingleCarViewModel>()
            .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x =>
                    $"/images/{GlobalConstants.CarsImagesFolder}/" + x.Images.FirstOrDefault().Id + "." +
                    x.Images.FirstOrDefault().Extension));
    }
}