namespace CarAuction.Data.Models.AuctionModels;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarAuction.Data.Common.Models;

public class Location : BaseDeletableModel<int>
{
    public Location()
    {
        this.AuctionsOnLocation = new HashSet<Auction>();
    }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    public virtual ICollection<Auction> AuctionsOnLocation { get; set; }
}
