using System.Threading.Tasks;

namespace CarAuction.Services
{
    public interface IAutoDataScraper
    {
        Task PopulateDbWithData();
    }
}