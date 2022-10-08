namespace CarAuction.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CarAuction.Data.Models.Enums;
    using CarAuction.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BaseCarInputModel : CarInputModelDropdownItems
    {
            [DisplayName("Start price")]
            public decimal StartPrice { get; set; }

            [DisplayName("Buy now price")]
            public decimal? BuyNowPrice { get; set; }

            [Range(2, 5)]
            [DisplayName("Count of doors")]
            public int ModelDoorsCount { get; set; } = 2;

            [Required]
            [DisplayName("Horse power")]
            public int ModelEngineHorsePower { get; set; }

            [Required]
            [ValidDate]
            [DisplayName("Year of production")]
            public int ModelYearOfProduction { get; set; } = DateTime.UtcNow.Year;

            [Required]
            public bool IsRunning { get; set; }

            [Required]
            [DisplayName("Mileage")]
            public long Milleage { get; set; }
    }
}
