using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;



public interface IUserMapings
{
    User UserCreateDtoToUser(UserCreateDto newUserDto);
    UserPresentLongDto UserToUserPresentLongDto(User createdUser);
    UserPresentShortDto UserToUserPresentShortDto(User user);
    AddressDto AddressToAddressDto(Address address);
    Address AddressDtoToAddress(AddressDto addressDto);
    PersonalInfoDto PersonalInfoToPersonalInfoDto(PersonalInfo personalInfo);
    PersonalInfo PersonalInfoDtoToPersonalInfo(PersonalInfoDto personalInfoDto);
}
