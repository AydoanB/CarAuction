using System.ComponentModel;

namespace CarAuction.Data.Models.Enums
{
    public enum DrivetrainType
    {
        [Description("Front-wheel drive")] fwd,
        [Description("Rear-wheel drive")] rwd,
        [Description("all-wheel drive")] awd,
    }
}
