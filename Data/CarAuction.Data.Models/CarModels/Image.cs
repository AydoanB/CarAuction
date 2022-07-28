using System.ComponentModel.DataAnnotations.Schema;

namespace CarAuction.Data.Models.CarModels;

public class Image
{
    [Key]
    public int Id { get; set; }

    public byte[] Bytes { get; set; }

    public string Description { get; set; }

    public string FileExtension { get; set; }

    public decimal Size { get; set; }

    public int CarId { get; set; }
    public virtual Car Car { get; set; }

}