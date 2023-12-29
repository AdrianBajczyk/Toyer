using Toyer.Data.Entities;
using Toyer.Logic.Dtos.DeviceType;

public interface IDeviceTypeMappings
{
    DeviceTypeCreateDto DeviceTypeToDeviceTypeCreateDto(DeviceType createdDeviceType);
    DeviceType DeviceTypeCreateDtoToDeviceType(DeviceTypeCreateDto newDeviceTypeDto);
    DeviceTypePresentDto DeviceTypeToDeviceTypePresentDto(DeviceType createdDeviceType);
}
