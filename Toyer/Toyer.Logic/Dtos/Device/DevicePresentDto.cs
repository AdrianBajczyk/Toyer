using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public record DevicePresentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfCreation { get; set; }
    public DateTime DateOfLastRegistration { get; set; }
    public DeviceTypePresentDto DeviceTypeDto { get; set; }

}
