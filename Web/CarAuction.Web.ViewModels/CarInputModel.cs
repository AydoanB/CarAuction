using Microsoft.AspNetCore.Http;

namespace CarAuction.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CarAuction.Data.Models.Enums;
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
        public decimal StartPrice { get; set; }

        [DisplayName("Buy now price")]
        public decimal? BuyNowPrice { get; set; }

        [Required]
        [DefaultValue("Black")]
        public string Color { get; set; }

        [Range(2, 5)]
        [DisplayName("Count of doors")]
        public int DoorsCount { get; set; } = 2;

        [Required]
        [DisplayName("Transmission")]
        public TransmissionType TransmissionType { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }

        [Required]
        [DisplayName("Drivetrain")]
        public DrivetrainType DrivetrainType { get; set; }
        public IEnumerable<SelectListItem> Drivetrains { get; set; }

        [Required]
        [DisplayName("Fuel")]
        public FuelType FuelType { get; set; }
        public IEnumerable<SelectListItem> Fuels { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        [ValidDate]
        public int Year { get; set; } = DateTime.UtcNow.Year;

        [Required]
        public bool IsRunning { get; set; }

        [Required]
        public long Milleage { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
