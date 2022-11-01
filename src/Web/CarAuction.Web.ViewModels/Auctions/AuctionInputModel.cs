namespace CarAuction.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CarAuction.Web.ViewModels.Locations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AuctionInputModel
    {
        public LocationInputModel Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }
    }
}
