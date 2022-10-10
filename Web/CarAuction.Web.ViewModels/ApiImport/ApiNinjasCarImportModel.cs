using CarAuction.Data.Models.CarModel;
using Newtonsoft.Json;

namespace CarAuction.Services
{
    public class ApiNinjasCarImportModel
    {
        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
    }
}
