namespace CarAuction.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CarAuction.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarInputModel
    {
        [DisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        [DisplayName("Model")]
        public int ModelId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Models { get; set; }

        [DisplayName("Engine")]
        public int EngineId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Engines { get; set; }

        [DisplayName("Start price")]
        [DefaultValue(1500)]
        public decimal StartPrice { get; set; }

        [Range(2, 5)]
        [DisplayName("Count of doors")]
        public int DoorsCount { get; set; } = 2;

        [Required]
        [DisplayName("Transmission")]
        public string TransmissionId { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }

        [Required]
        [ValidDate]
        public int Year { get; set; } = DateTime.UtcNow.Year;
    }
}
