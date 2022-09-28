﻿namespace CarAuction.Data.Models.AuctionModels;

using System;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;

public class  Bid : BaseDeletableModel<string>
{
    public Bid()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    public int CarId { get; set; }
    public Car Car { get; set; }

    [Required]
    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    [Column(TypeName = "decimal(4,0)")]
    public decimal AmountOfBid { get; set; }

    public bool IsBuyNow { get; set; }
}
