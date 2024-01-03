using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Mappings.UserMappings.classes;

public class DeviceMappings : IDeviceMappings
{
    private readonly IMapper _mapper;

    public DeviceMappings(IMapper mapper)
    {
        _mapper = mapper;
    }
    public Device DeviceCreateDtoToDevice(DeviceCreateDto deviceCreateDto) => _mapper.Map<Device>(deviceCreateDto);
    public Device DeviceNameUpdateDtoToDevice(DeviceNameUpdateDto deviceNameUpdateDto) => _mapper.Map<Device>(deviceNameUpdateDto);
    public DevicePresentDto DeviceToDevicePresentDto(Device device) 
    { 
        var devicePresentDto = _mapper.Map<DevicePresentDto>(device);
        devicePresentDto.DeviceTypeDto = _mapper.Map<DeviceTypePresentDto>(device.DeviceType);
        devicePresentDto.DateOfCreation = device.CreationDate;

        return devicePresentDto;
    } 
}
