namespace CarAuction.Web.ViewModels
{
    using System;

    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Services.Mapping;

    public class AuctionViewModel : IMapFrom<Auction>
    {
        public string LocationName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
