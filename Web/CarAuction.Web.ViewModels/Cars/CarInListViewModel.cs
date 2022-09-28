using CarAuction.Data.Models.Enums;

namespace CarAuction.Web.ViewModels
{
    using System.Linq;

    using AutoMapper;
    using CarAuction.Common;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Services.Mapping;

    public class CarInListViewModel : IMapFrom<Car>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyNowPrice { get; set; }
        public long Milleage { get; set; }

        public VehicleType ModelVehicleType { get; set; }

        public int ModelYearOfProduction { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Car, CarInListViewModel>()
                .ForMember(
                    x => x.Title,
                    opt =>
                        opt.MapFrom(x => $"{x.Model.Manufacturer.Name} - {x.Model.Name} - {x.Model.Engine.Name}"))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x => $"/images/{GlobalConstants.CarsImagesFolder}/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }

        // .ForMember(x => x.PosterUrl, opt =>
        // opt.MapFrom(x => x.PosterPath == null ?
        // GlobalConstants.BlankMoviePoster :
        // x.PosterPath.Contains("-") ?
        // $"/images/{GlobalConstants.MoviesImagesFolder}/" + x.PosterPath :
        // string.Format(GlobalConstants.RemoteImagesUrl, x.PosterPath)))
    }
}
