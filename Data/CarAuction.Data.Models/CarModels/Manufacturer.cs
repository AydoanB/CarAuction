using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarAuction.Data.Models.CarModel;

public class Manufacturer
{
    public Manufacturer()
    {
        Models = new HashSet<Model>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}