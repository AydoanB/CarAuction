using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }

    public GearBox GearBox { get; set; }

    public int Capacity { get; set; }

    public int Cylinders { get; set; }

    public string FuelType { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}