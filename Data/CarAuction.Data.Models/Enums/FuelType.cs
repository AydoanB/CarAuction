namespace CarAuction.Data.Models.Enums
{
    using System.ComponentModel;

    public enum FuelType
    {
        [Description("Gas")] gas,
        [Description("Diesel")] diesel,
        [Description("Electricity")] electricity,
    }
}