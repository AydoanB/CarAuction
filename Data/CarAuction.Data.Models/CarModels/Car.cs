using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CarAuction.Data.Common.Models;
using CarAuction.Data.Models.AuctionModels;
using CarAuction.Data.Models.CarModels;

namespace CarAuction.Data.Models.CarModel;

public class Car : BaseDeletableModel<int>
{
    public Car()
    {
        Accessories = new HashSet<Accessory>();
        Bids = new HashSet<Bid>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    public int ModelId { get; set; }
    public virtual Model Model { get; set; }

    [Required]
    public int AuctionId { get; set; }
    public virtual Auction Auction { get; set; }

    [Required]
    [Column(TypeName = "decimal(4,0)")]
    public decimal StartingPrice { get; set; }

    [Column(TypeName = "decimal(4,0)")]
    public decimal? BuyNowPrice { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Color { get; set; }

    [Required]
    public long Milleage { get; set; }

    [Required]
    [StringLength(14)]
    public string VIN { get; set; }

    public bool IsRunning { get; set; }

    //TODO Images Property

    public virtual ICollection<Accessory> Accessories { get; set; }

    public virtual ICollection<Bid> Bids { get; set; }
}