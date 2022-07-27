using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarAuction.Data.Models.CarModel;

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
    public string Name { get; set; }

    public virtual ICollection<Car> CarsWithAccessory { get; set; }
}