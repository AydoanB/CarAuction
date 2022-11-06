namespace CarAuction.Web.ViewModels.Cars;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

public class CarsListViewModel : PagingViewModel
{
    public IEnumerable<CarInListViewModel> Cars { get; set; }

    public SearchCarInputModel SearchModel { get; set; }

    public string Order { get; set; }

    public IEnumerable<SelectListItem> OrderOptions { get; set; }
}
