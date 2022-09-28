using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CarAuction.Data.Models.CarModels;

using System;

using CarAuction.Data.Common.Models;

public class Image : BaseDeletableModel<string>
{
    public Image()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public string RemoteImageUrl { get; set; }

    public string Extension { get; set; }

    public string AddedByUserId { get; set; }

    public virtual ApplicationUser AddedByUser { get; set; }
}
