namespace CarAuction.Services
{
    using System.Threading.Tasks;

    public interface IAutoDataScraper
    {
        Task PopulateDbWithDataWithScraping();
        Task PopulateDbWithDataFromApi(int limit);

    }
}
