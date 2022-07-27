using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarAuction.Data.Models.AuctionModels;

public class Location
{
    public Location()
    {
        this.AuctionsOnLocation = new HashSet<Auction>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ICollection<Auction> AuctionsOnLocation { get; set; }
}