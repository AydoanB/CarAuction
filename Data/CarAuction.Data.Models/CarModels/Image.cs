namespace CarAuction.Data.Models.CarModels;

using System;

using CarAuction.Data.Common.Models;

public class Image : BaseModel<string>
{
    public Image()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public byte[] Bytes { get; set; }

    public string Description { get; set; }

    public string FileExtension { get; set; }

    public decimal Size { get; set; }

    public int CarId { get; set; }
    public virtual Car Car { get; set; }
}
