using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;

public interface IDeviceMappings
{
    Device DeviceCreateDtoToDevice(DeviceCreateDto deviceCreateDto);
    DevicePresentDto DeviceToDevicePresentDto(Device device);
    Device DeviceNameUpdateDtoToDevice(DeviceNameUpdateDto deviceNameUpdateDto);
}
