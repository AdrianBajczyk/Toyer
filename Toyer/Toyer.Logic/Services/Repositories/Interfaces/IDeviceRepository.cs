using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Repositories.Interfaces;

interface IDeviceRepository
{
   Task<DeviceCreateDto> CreateNewDeviceAsync(DeviceCreateDto deviceCreateInfo);
   Task<DevicePresentDto> UpdateDeviceName(DeviceNameUpdateDto nameUpdateDto);
   Task<DevicePresentDto> UpdateApStrings(DeviceAPConnectionDto oldApStrings, DeviceAPConnectionDto newApStrings);
   Task<DevicePresentDto> UpdateStaStrings(DeviceStaConnectionDto oldStaStrings, DeviceStaConnectionDto newStaStrings);
}
