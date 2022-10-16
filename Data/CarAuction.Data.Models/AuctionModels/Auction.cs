namespace CarAuction.Data.Models.AuctionModels;

using System;
using System.Collections.Generic;

using CarAuction.Data.Common.Models;

public class Auction : BaseDeletableModel<int>
{
    public Auction()
    {
        this.CarsInAuction = new HashSet<Car>();
    }

    [Required]
    public int LocationId { get; set; }
    public Location Location { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Car> CarsInAuction { get; set; }
}
