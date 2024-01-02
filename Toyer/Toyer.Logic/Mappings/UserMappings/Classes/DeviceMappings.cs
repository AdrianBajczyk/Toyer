using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;

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
    public DevicePresentDto DeviceToDevicePresentDto(Device device) => _mapper.Map<DevicePresentDto>(device);
}
