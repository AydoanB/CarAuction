namespace CarAuction.Data.Models.CarModel;

using System;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;
using CarAuction.Data.Models.Enums;

public class Model : BaseDeletableModel<int>
{
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    public int? YearOfProduction { get; set; }

    public VehicleType? VehicleType { get; set; }

    public DrivetrainType? Drivetrain { get; set; }

    public int? ManufacturerId { get; set; }
    public virtual Manufacturer? Manufacturer { get; set; }

    public int? EngineId { get; set; }
    public virtual Engine? Engine { get; set; }
}
