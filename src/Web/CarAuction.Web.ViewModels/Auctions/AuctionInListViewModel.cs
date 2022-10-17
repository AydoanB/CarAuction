namespace CarAuction.Web.ViewModels
{
    using System;

    using AutoMapper;
    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Services.Mapping;

    public class AuctionInListViewModel : IMapFrom<Auction>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CarsInAuctionCount { get; set; }

        public string CreatorName { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Auction, AuctionInListViewModel>()
                .ForMember(
                    x => x.CreatorName,
                    opt => opt.MapFrom(d => d.User.UserName));
        }
    }
}
