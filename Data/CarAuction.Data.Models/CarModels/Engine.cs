namespace CarAuction.Data.Models.CarModel;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;
using Enums;

public class Engine : BaseDeletableModel<string>
{
    public Engine()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Models = new HashSet<Model>();
    }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }

    public GearBox GearBox { get; set; }

    public int Capacity { get; set; }

    public int Cylinders { get; set; }

    public string FuelType { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}
