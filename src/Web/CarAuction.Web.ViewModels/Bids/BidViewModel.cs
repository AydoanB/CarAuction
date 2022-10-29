namespace CarAuction.Web.ViewModels.Bids
{
    using System;

    using AutoMapper;
    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Services.Mapping;

    public class BidViewModel : IMapFrom<Bid>, IHaveCustomMappings
    {
        public string UserName { get; set; }
        public string CreatedOn { get; set; }
        public decimal AmountOfBid { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Bid, BidViewModel>()
                .ForMember(
                    x => x.UserName,
                    opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(
                    x => x.CreatedOn,
                    opt => opt.MapFrom(x => x.CreatedOn.ToString("g")));
        }
    }
}
