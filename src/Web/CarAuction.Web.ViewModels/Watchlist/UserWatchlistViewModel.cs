namespace CarAuction.Web.ViewModels.Watchlist
{
    using System.Collections.Generic;
    using CarAuction.Web.ViewModels.Cars;

    public class UserWatchlistViewModel
    {
        public IEnumerable<CarInWatchlistViewModel> WatchedCars { get; set; }
    }
}
