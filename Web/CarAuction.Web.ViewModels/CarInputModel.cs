namespace CarAuction.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CarAuction.Data.Models.Enums;
    using CarAuction.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarInputModel
    {
        [DisplayName("Manufacturer")]
        [Required]
        public int ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        [Required]
        [DisplayName("Model")]
        public int ModelId { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        [DisplayName("Engine")]
        [Required]
        public int EngineId { get; set; }
        public IEnumerable<SelectListItem> Engines { get; set; }

        [DisplayName("Start price")]
        public decimal StartPrice { get; set; }

        [DisplayName("Buy now price")]
        public decimal? BuyNowPrice { get; set; }

        [DefaultValue(Color.Black)]
        public Color ColorType { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }

        public VehicleType VehicleType { get; set; }
        public IEnumerable<SelectListItem> Vehicles { get; set; }

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
        [DisplayName("Mileage")]
        public long Milleage { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        [DisplayName("Auction")]
        public int AuctionId { get; set; }

        public IEnumerable<SelectListItem> Auctions { get; set; }
    }
}
