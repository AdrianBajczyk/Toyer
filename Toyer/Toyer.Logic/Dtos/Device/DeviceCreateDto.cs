using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public record DeviceCreateDto
{
    public string Name { get; set; }
    public DeviceTypeDto DeviceTypeDto{ get; set; }
    public string StaSsid { get; set; }
    public string StaPass { get; set; }
    public DateOnly DateOfCreation { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    public string Mac { get; set; }
}
