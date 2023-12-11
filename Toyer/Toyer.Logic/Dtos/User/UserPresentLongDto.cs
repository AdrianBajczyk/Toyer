using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Dtos.User;



public class UserPresentLongDto
{
    public UserPresentShortDto UserPresentShort { get; set; }
    public UserPersonalInfoDto UserPersonalInfo { get; set; }
    public UserAddressDto UserAddress { get; set; }
    public DateTime AccCreationDate { get; set; }
    public List<DevicePresentDto>? Devices { get; set; } 
}
