namespace CarAuction.Web.ViewModels.Cars
{
    using System.Linq;

    using AutoMapper;
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Services.Mapping;

    using static CarAuction.Common.GlobalConstants;

    public class WatchlistCarViewModel : IMapFrom<Car>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ModelManufacturerName { get; set; }

        public string ModelName { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyNowPrice { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Car, SingleCarViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        $"/images/{CarsResizedImagesFolder}/" + x.Images.FirstOrDefault().Id + "." +
                        x.Images.FirstOrDefault().Extension));
        }
    }
}
