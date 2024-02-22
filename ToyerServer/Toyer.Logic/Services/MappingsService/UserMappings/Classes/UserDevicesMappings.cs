using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Mappings.UserMappings
{
    public class UserDevicesMappings(IMapper mapper) : IUserDevicesMappings
    {
        private readonly IMapper _mapper = mapper;

        public IEnumerable<DevicePresentShortDto> UserDevicesToUserDevicesPresentDto(UserDevices userDevices)
        {
            var devices = (IEnumerable<Device>)userDevices.Devices;
            return _mapper.Map<IEnumerable<DevicePresentShortDto>>(devices);
        }
    }
}