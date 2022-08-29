using System;

namespace CarAuction.Services.Data.JsonImport
{
    public class CarsImportDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Fuel_Type { get; set; }

        public string Class { get; set; }

        public string Drive { get; set; }

        public double Displacement { get; set; }

        public double Cylinders { get; set; }

        public string Transmission { get; set; }

        public string Year { get; set; }
    }
}