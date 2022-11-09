namespace CarAuction.Web.ViewModels.Watchlist
{
    using System.Collections.Generic;
    using CarAuction.Web.ViewModels.Cars;

    public class SimpleCarListViewModel
    {
        public IEnumerable<SimpleCarDetailsViewModel> WatchedCars { get; set; }
    }
}
