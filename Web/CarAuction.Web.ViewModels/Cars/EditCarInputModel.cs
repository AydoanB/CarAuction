namespace CarAuction.Web.ViewModels
{
    using CarAuction.Data.Models.CarModel;
    using CarAuction.Services.Mapping;

    public class EditCarInputModel : BaseCarInputModel, IMapFrom<Car>
    {
        public int Id { get; set; }
    }
}
