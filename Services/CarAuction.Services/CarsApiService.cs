using System.Collections.Generic;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;
using Newtonsoft.Json;
using RestSharp;

namespace CarAuction.Services
{
    public class CarsApiService
    {
        public async Task AddCars(string make, int limit)
        {
            var client = new RestClient();

            var url = $"https://api.api-ninjas.com/v1/cars?limit={limit}" + $"&make={make}";


            var request = new RestRequest(url);
            request.AddHeader("x-api-key", "c9f5dleiao8N6d+3LmsrmA==qf17CKr6PhhuleZD");

            var response = client.Get(request);
            //TODO Response is done, problem with json deserializing
            
            var serialized = JsonConvert.DeserializeObject<List<Car>>(response.Content);

            foreach (var carDto in serialized)
            {
                var car = new Car();
            }
        }
    }
}