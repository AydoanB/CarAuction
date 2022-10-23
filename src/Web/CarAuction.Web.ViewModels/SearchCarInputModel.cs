using System.Collections.Generic;
using CarAuction.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarAuction.Web.ViewModels
{
    public class SearchCarInputModel
    {
        public string Search { get; set; }

        public int ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> ManufacturerItems { get; set; }

        public int ModelId { get; set; }
        public IEnumerable<SelectListItem> ModelsItems { get; set; }

        public decimal? StartPriceFrom { get; set; }

        public decimal? StartPriceTo { get; set; }

        public int AuctionId { get; set; }
        public IEnumerable<SelectListItem> AuctionItems { get; set; }

    }
}
