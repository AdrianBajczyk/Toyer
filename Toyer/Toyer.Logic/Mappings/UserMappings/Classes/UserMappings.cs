using AutoMapper;
using System.Net;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Mappings.UserMappings.classes;

public class UserMappings : IUserMapings
{
    private readonly IMapper _mapper;

    public UserMappings(IMapper mapper)
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

    public UserPresentLongDto UserToUserPresentLongDto(User createdUser)
    {
        var userLongDto = _mapper.Map<UserPresentLongDto>(createdUser);
        userLongDto.UserPersonalInfo = _mapper.Map<PersonalInfoDto>(createdUser.PersonalInfo);
        userLongDto.UserAddress = _mapper.Map<AddressDto>(createdUser.PersonalInfo!.Address);
        userLongDto.UserPresentShort = _mapper.Map<UserPresentShortDto>(createdUser);
        return userLongDto;
    }

    public UserPresentShortDto UserToUserPresentShortDto(User user) => _mapper.Map<UserPresentShortDto>(user);
    public AddressDto AddressToAddressDto(Address address) => _mapper.Map<AddressDto>(address);
    public Address AddressDtoToAddress(AddressDto addressDto) => _mapper.Map<Address>(addressDto);
    public PersonalInfoDto PersonalInfoToPersonalInfoDto(PersonalInfo personalInfo) => _mapper.Map<PersonalInfoDto>(personalInfo);
    public PersonalInfo PersonalInfoDtoToPersonalInfo(PersonalInfoDto personalInfoDto) => _mapper.Map<PersonalInfo>(personalInfoDto);
    public IEnumerable<UserPresentShortDto> UsersToUserPresentShortDtos(IEnumerable<User> users) => _mapper.Map<IEnumerable<UserPresentShortDto>>(users);
}
