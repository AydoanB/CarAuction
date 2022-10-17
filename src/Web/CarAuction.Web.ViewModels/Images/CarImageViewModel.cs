namespace CarAuction.Web.ViewModels.Images
{
    using AutoMapper;
    using CarAuction.Common;
    using CarAuction.Data.Models.CarModels;
    using CarAuction.Services.Mapping;

    public class CarImageViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string PhotoUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Image, CarImageViewModel>()
                .ForMember(x => x.PhotoUrl, opt => opt.MapFrom(x =>
                    $"/images/{GlobalConstants.CarsImagesFolder}/" + x.Id + "." +
                    x.Extension));
        }
    }
}