using CarAuction.Data.Models.Enums;

namespace CarAuction.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarInputModelDropdownItems
    {
        [DisplayName("Manufacturer")]
        [Required]
        public int ModelManufacturerId { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        [Required]
        [DisplayName("Model")]
        public int ModelModelId { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        [DisplayName("Engine")]
        [Required]
        public int EngineId { get; set; }
        public IEnumerable<SelectListItem> Engines { get; set; }

        public Color Color { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }

        [DisplayName("Vehicle type")]
        public VehicleType ModelVehicleType { get; set; }
        public IEnumerable<SelectListItem> Vehicles { get; set; }

        [Required]
        [DisplayName("Drivetrain")]
        public DrivetrainType ModelDrivetrainType { get; set; }
        public IEnumerable<SelectListItem> Drivetrains { get; set; }

        [Required]
        [DisplayName("Fuel")]
        public FuelType ModelEngineFuelType { get; set; }
        public IEnumerable<SelectListItem> Fuels { get; set; }

        [Required]
        [DisplayName("Transmission")]
        public TransmissionType ModelEngineTransmissionType { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }

        [DisplayName("Auction")]
        public int AuctionId { get; set; }

        public IEnumerable<SelectListItem> Auctions { get; set; }
    }
}
