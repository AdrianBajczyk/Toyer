using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public class DeviceCreateDto
{
    public DeviceTypeDto DeviceTypeDto{ get; set; }
    public DateOnly DateOfCreation { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    public string Mac { get; set; }
}
