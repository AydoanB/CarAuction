using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarAuction.Data.Models.CarModels;

public class Accessory
{
    public Accessory()
    {
        this.CarsWithAccessory = new HashSet<Car>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    public virtual ICollection<Car> CarsWithAccessory { get; set; }
}