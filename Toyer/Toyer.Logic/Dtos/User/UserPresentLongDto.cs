using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Dtos.User;



public class UserPresentLongDto
{
    public UserPresentShortDto UserPresentShort { get; set; }
    public UserPersonalInfoDto UserPersonalInfo { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public List<DevicePresentDto>? Devices { get; set; } 
}
