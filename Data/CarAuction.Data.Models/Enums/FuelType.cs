namespace CarAuction.Data.Models.Enums
{
    using System.ComponentModel;

    public enum FuelType
    {
        [Description("Gas")] Gas,
        [Description("Diesel")] Diesel,
        [Description("Electricity")] Electricity,
    }
}