using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;

public interface IUserDevicesMappings
{
    IEnumerable<DevicePresentShortDto> UserDevicesToUserDevicesPresentDto(UserDevices userDevices);
}