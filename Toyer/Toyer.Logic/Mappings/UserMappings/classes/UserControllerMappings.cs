using AutoMapper;
using System.Net;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Mappings.UserMappings.classes;

public class UserControllerMappings : IUserControllerMapings
{
    private readonly IMapper _mapper;

    public UserControllerMappings(IMapper mapper)
    {
        _mapper = mapper;
    }

    public User UserCreateDtoToUser(UserCreateDto newUserDto)
    {
        var userToCreate = _mapper.Map<User>(newUserDto);
        var personalInfoTo = _mapper.Map<PersonalInfo>(newUserDto);
        var addressToDb = _mapper.Map<Address>(newUserDto);

        personalInfoTo.Address = addressToDb;
        userToCreate.PersonalInfo = personalInfoTo;

        return userToCreate;
    }

    public UserPresentLongDto UserToUserPresentLongDto(User? createdUser)
    {
        var userLongDto = _mapper.Map<UserPresentLongDto>(createdUser);
        userLongDto.UserPersonalInfo = _mapper.Map<PersonalInfoDto>(createdUser.PersonalInfo);
        userLongDto.UserAddress = _mapper.Map<AddressDto>(createdUser.PersonalInfo.Address);
        userLongDto.UserPresentShort = _mapper.Map<UserPresentShortDto>(createdUser);
        return userLongDto;
    }

    public UserPresentShortDto UserToUserPresentShortDto(User user)
    {
        return _mapper.Map<UserPresentShortDto>(user);
    }

    public AddressDto AddressToAddressDto(Address address)
    {
        return _mapper.Map<AddressDto>(address);
    }
    public Address AddressDtoToAddress(AddressDto addressDto)
    {
        return _mapper.Map<Address>(addressDto);
    }

    public PersonalInfoDto PersonalInfoToPersonalInfoDto(PersonalInfo personalInfo)
    {
        return _mapper.Map<PersonalInfoDto>(personalInfo);
    }

    public PersonalInfo PersonalInfoDtoToPersonalInfo(PersonalInfoDto personalInfoDto)
    {
        return _mapper.Map<PersonalInfo>(personalInfoDto);
    }
}
