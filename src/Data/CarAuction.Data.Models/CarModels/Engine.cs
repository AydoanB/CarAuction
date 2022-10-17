namespace CarAuction.Data.Models.CarModel;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;
using CarAuction.Data.Models.Enums;

public class Engine : BaseDeletableModel<int>
{
    public Engine()
    {
        this.Models = new HashSet<Model>();
    }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; }

    public TransmissionType TransmissionType { get; set; }

    public int HorsePower { get; set; }

    public FuelType FuelType { get; set; }

    public virtual ICollection<Model> Models { get; set; }
}
