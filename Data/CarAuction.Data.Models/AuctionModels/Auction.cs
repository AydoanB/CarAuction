using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarAuction.Data.Models.CarModel;

namespace CarAuction.Data.Models.AuctionModels;

public class Auction
{
    public Auction()
    {
        this.CarsInAuction = new HashSet<Car>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    public int LocationId { get; set; }
    public Location Location { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public virtual ICollection<Car> CarsInAuction { get; set; }
}