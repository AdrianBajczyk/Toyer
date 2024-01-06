using Toyer.Data.Entities;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   Task<User?> CreateNewUserAsync(User newUser);
   Task<User?> GetUserByIdAsync(Guid Id);
   Task<List<User>?> GetUsersAsync();
   Task<Address?> UpdateAddressPatchAsync(Guid userId, Address updatesFromUserDocument);
   Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, PersonalInfo updatesFromUser);
   Task<User?> DeleteUserAsync(Guid Id);
   Task<CustomResponse> AssignDeviceToUserAsync(Guid userId, Guid deviceId);
}
