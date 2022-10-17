using CarAuction.Data.Models.CarModel;
using CarAuction.Services.Mapping;

namespace CarAuction.Web.ViewModels;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using CarAuction.Data.Models.Enums;
using CarAuction.Web.Infrastructure.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CarInputModel : BaseCarInputModel
{
    public IEnumerable<IFormFile> Images { get; set; }

}
