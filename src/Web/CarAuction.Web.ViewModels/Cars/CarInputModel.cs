namespace CarAuction.Web.ViewModels;

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

public class CarInputModel : BaseCarInputModel
{
    public IEnumerable<IFormFile> Images { get; set; }
}
