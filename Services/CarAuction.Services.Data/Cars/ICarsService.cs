using System;
using System.Collections.Generic;
using CarAuction.Web.ViewModels;

namespace CarAuction.Services.Data.Cars
{
    public interface ICarsService
    {
        IEnumerable<ShowCarsViewModel> ShowAll();
    }
}

