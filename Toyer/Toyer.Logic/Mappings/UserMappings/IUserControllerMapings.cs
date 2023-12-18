using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;



public interface IUserControllerMapings
{
    User MapUserCreateDtoToUser(UserCreateDto newUserDto);
    UserPresentLongDto MapUserDataToLongDto(User? createdUser);
}
