namespace CarAuction.Web.ViewModels.Watchlist
{
    using System.Collections.Generic;

    using CarAuction.Web.ViewModels.Cars;

    public class UserWatchlistViewModel
    {
        public ICollection<WatchlistCarViewModel> Cars { get; set; }
    }
}