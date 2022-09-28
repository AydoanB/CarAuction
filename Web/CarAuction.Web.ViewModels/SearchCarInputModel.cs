using System.Collections.Generic;
using CarAuction.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarAuction.Web.ViewModels
{
    public class SearchCarInputModel
    {
        public string Search { get; set; }

        public int[] MakesIds { get; set; }
        public IEnumerable<SelectListItem> MakesItems { get; set; }

        public int[] ModelsIds { get; set; }
        public IEnumerable<SelectListItem> ModelsItems { get; set; }

        public VehicleType[] VehicleTypes { get; set; }
        public IEnumerable<SelectListItem> VehicleTypesItems { get; set; }

        public bool[] ConditionsIds { get; set; }

        public TransmissionType[] TransmissionTypes { get; set; }
        public IEnumerable<SelectListItem> TransmissionTypeItems { get; set; }

        public decimal? StartPriceFrom { get; set; }

        public decimal? StartPriceTo { get; set; }

    }
}
