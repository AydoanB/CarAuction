namespace CarAuction.Data.Models.CarModel;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;

public class Manufacturer : BaseDeletableModel<int>
{
    public Manufacturer()
    {
        this.Models = new HashSet<Model>();
    }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}
