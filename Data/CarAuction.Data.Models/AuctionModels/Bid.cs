using CarAuction.Data.Models.CarModel;
using System.ComponentModel.DataAnnotations;

namespace CarAuction.Data.Models.AuctionModels;

public class Bid
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CarId { get; set; }
    public Car Car { get; set; }

    [Required]
    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public decimal AmountOfBid { get; set; }

    public bool IsBuyNow { get; set; }
}