using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarAuction.Data.Models.Enums;

namespace CarAuction.Data.Models.CarModel;

public class Engine
{
    public Engine()
    {
        Models = new HashSet<Model>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public GearBox GearBox { get; set; }

    public int? Capacity { get; set; }

    public FuelType FuelType { get; set; }

    public string EngineType { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}