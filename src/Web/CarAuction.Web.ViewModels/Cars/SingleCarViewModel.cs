namespace CarAuction.Web.ViewModels;

using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using CarAuction.Data.Models.CarModel;
using CarAuction.Data.Models.Enums;
using CarAuction.Services.Mapping;
using CarAuction.Web.ViewModels.Bids;
using CarAuction.Web.ViewModels.Images;

using static CarAuction.Common.GlobalConstants;

public class SingleCarViewModel : IMapFrom<Car>, IHaveCustomMappings
{
    public string UserId { get; set; }

    public bool IsAbleToEditAndDelete { get; set; }
    public bool IsInUsersWatchlist { get; set; }

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

    public AuctionViewModel Auction { get; set; }

    public IEnumerable<CarImageViewModel> Images { get; set; }

    public IEnumerable<BidViewModel> Bids { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Car, SingleCarViewModel>()
            .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x =>
                    $"/images/{CarsResizedImagesFolder}/" + x.Images.FirstOrDefault().Id + "." +
                    x.Images.FirstOrDefault().Extension));
    }
}
