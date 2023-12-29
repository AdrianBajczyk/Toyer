using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Mappings.UserMappings.classes;

public class DeviceTypeMappings : IDeviceTypeMappings
{
    private readonly IMapper _mapper;

    public DeviceTypeMappings(IMapper mapper)
    {
        _mapper = mapper;
    }

    public DeviceTypeCreateDto DeviceTypeToDeviceTypeCreateDto(DeviceType? deviceType)
    {
        return _mapper.Map<DeviceTypeCreateDto>(deviceType);
    }

    public DeviceType DeviceTypeCreateDtoToDeviceType(DeviceTypeCreateDto deviceTypeDto)
    {
        return _mapper.Map<DeviceType>(deviceTypeDto);
    }

    public DeviceTypePresentDto DeviceTypeToDeviceTypePresentDto(DeviceType deviceType)
    {
        return _mapper.Map<DeviceTypePresentDto>(deviceType);
    }
    public IEnumerable<DeviceTypePresentDto> DeviceTypesToDeviceTypePresentDtos(IEnumerable<DeviceType> deviceTypes)
    {
        return _mapper.Map<IEnumerable<DeviceTypePresentDto>>(deviceTypes);
    }

}