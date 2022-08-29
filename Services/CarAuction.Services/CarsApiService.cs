using System.Collections.Generic;
using System.Threading.Tasks;
using CarAuction.Data.Models.CarModel;
using Newtonsoft.Json;
using RestSharp;

namespace CarAuction.Services
{
    public class CarsApiService
    {
        public async Task<IEnumerable<Car>> AddCars(string make, int limit)
        {
            var client = new RestClient("https://cars-by-api-ninjas.p.rapidapi.com/v1/cars?model=corolla");

            var request = new RestRequest();

            request.AddHeader("X-RapidAPI-Key", "574c62cd9cmsh104458aa8847d87p1b1cc6jsn464602503935");
            request.AddHeader("X-RapidAPI-Host", "cars-by-api-ninjas.p.rapidapi.com");

            var response = client.Execute(request);

            var serialized = JsonConvert.DeserializeObject<List<Car>>(response.Content);

            return serialized;
        }
    }
}