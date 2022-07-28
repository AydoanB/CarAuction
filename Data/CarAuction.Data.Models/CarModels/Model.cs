using System.ComponentModel.DataAnnotations.Schema;
using CarAuction.Data.Models.Enums;

namespace CarAuction.Data.Models.CarModel;

public class Model
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }

    [Required]
    public int YearOfProduction { get; set; }

    [Required]
    public Doors CountOfDoors { get; set; }

    public CarType VehicleType { get; set; }

    public int? EuroStandart { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string Drivetrain { get; set; }

    [Required]
    public int ManufacturerId { get; set; }
    public virtual Manufacturer Manufacturer { get; set; }

    [Required]
    public int EngineId { get; set; }
    public virtual Engine Engine { get; set; }
}