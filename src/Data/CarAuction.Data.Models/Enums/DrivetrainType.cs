using System.ComponentModel;

namespace CarAuction.Data.Models.Enums
{
    public enum DrivetrainType
    {
        [Description("Front-wheel drive")] Fwd,
        [Description("Rear-wheel drive")] Rwd,
        [Description("all-wheel drive")] Awd,
    }
}
