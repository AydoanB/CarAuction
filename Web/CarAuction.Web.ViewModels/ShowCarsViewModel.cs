using System.Collections;

namespace CarAuction.Web.ViewModels
{
    public class ShowCarsViewModel
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal? StartPrice { get; set; }
        public int DoorsCount { get; set; }
        public string Transmission { get; set; }
    }
}