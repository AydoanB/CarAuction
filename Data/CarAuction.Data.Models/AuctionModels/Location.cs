using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    public virtual ICollection<Auction> AuctionsOnLocation { get; set; }
}