namespace CarAuction.Web.ViewModels
{
    using System.Collections.Generic;

    public class AuctionListViewModel : PagingViewModel
    {
        public IEnumerable<AuctionInListViewModel> Auctions { get; set; }
        public string Order { get; set; }
    }
}
