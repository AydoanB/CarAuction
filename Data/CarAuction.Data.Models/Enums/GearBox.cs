using System.ComponentModel;

namespace CarAuction.Data.Models.Enums;

public enum GearBox
{
    [Description("Automatic transmission")] Automatic,
    [Description("Manual transmission")] Manual,
    [Description("Semi-automatic")] SemiAuto,
    [Description("Continuosly variable transmission")]Gearless
}