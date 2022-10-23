namespace CarAuction.Web.ViewModels
{
    using System.Collections.Generic;

    using CarAuction.Web.ViewModels.Cars;

    public class RandomCarsViewModel
    {
        public IEnumerable<CarInListViewModel> Cars { get; set; }
    }
}
