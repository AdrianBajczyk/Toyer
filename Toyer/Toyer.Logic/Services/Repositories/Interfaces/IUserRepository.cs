using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   Task<User?> CreateNewUserAsync(User newUser);
   Task<User?> GetUserByIdAsync(Guid Id);
   Task<Address?> UpdateAddressPatchAsync(Guid userId, Address updatesFromUserDocument);
   Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, PersonalInfo updatesFromUser);
   Task<User?> DeleteUserAsync(Guid Id);
   Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
   Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
}
