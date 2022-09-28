using System.Collections.Generic;

namespace CarAuction.Services.Data.JsonImport
{
    public class CarsImportDto
    {
        public CarsImportDto()
        {
            this.Models = new HashSet<string>();
        }

        public string Make { get; set; }

        public ICollection<string> Models { get; set; }
    }
}
