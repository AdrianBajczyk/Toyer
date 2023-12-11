using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceRepository
{
   public Task<DeviceCreateDto> CreateNewDeviceAsync(DeviceCreateDto deviceCreateInfo);
   public Task<DevicePresentDto> UpdateDeviceName(DeviceNameUpdateDto nameUpdateDto);
   public Task<DevicePresentDto> UpdateApStrings(DeviceAPConnectionDto oldApStrings, DeviceAPConnectionDto newApStrings);
   public Task<DevicePresentDto> UpdateStaStrings(DeviceStaConnectionDto oldStaStrings, DeviceStaConnectionDto newStaStrings);
}
