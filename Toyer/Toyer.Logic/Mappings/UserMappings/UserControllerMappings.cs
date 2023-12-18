using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Mappings.UserMappings;

public class UserControllerMappings : IUserControllerMapings
{
    private readonly IMapper _mapper;

    public UserControllerMappings(IMapper mapper)
    {
        _mapper = mapper;
    }

    public User MapUserCreateDtoToUser(UserCreateDto newUserDto)
    {
        var userToCreate = _mapper.Map<User>(newUserDto);
        var personalInfoTo = _mapper.Map<PersonalInfo>(newUserDto);
        var addressToDb = _mapper.Map<Address>(newUserDto);

        personalInfoTo.Address = addressToDb;
        userToCreate.PersonalInfo = personalInfoTo;

        return userToCreate;
    }

    public UserPresentLongDto MapUserDataToLongDto(User? createdUser)
    {
        var userLongDto = _mapper.Map<UserPresentLongDto>(createdUser);
        userLongDto.UserPersonalInfo = _mapper.Map<UserPersonalInfoDto>(createdUser.PersonalInfo);
        userLongDto.UserAddress = _mapper.Map<UserAddressDto>(createdUser.PersonalInfo.Address);
        userLongDto.UserPresentShort = _mapper.Map<UserPresentShortDto>(createdUser);
        return userLongDto;
    }

}
