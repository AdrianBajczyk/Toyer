using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public class DevicePresentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfCreation { get; set; }
    public DateTime DateOfLastRegistration { get; set; }
    public DeviceTypeDto DeviceTypeDto { get; set; }
}
