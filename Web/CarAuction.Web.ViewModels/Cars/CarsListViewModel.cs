using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarAuction.Web.ViewModels;

public class CarsListViewModel : PagingViewModel
{
    public IEnumerable<CarInListViewModel> Cars { get; set; }

    public SearchCarInputModel SearchModel { get; set; }

    public string Order { get; set; }

    public IEnumerable<SelectListItem> OrderOptions { get; set; }
}