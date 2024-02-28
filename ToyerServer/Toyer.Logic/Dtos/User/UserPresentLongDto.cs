namespace Toyer.Logic.Dtos.User;



public class UserPresentLongDto
{
    public UserPresentShortDto UserPresentShort { get; set; }
    public PersonalInfoDto UserPersonalInfo { get; set; }
    public AddressDto UserAddress { get; set; }
    public DateTime AccCreationDate { get; set; }

}
