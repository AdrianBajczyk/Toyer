using Microsoft.AspNetCore.JsonPatch;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   public Task<User?> CreateNewUserAsync(User newUser, PersonalInfo newPersonalInfo, Address newAddress);
   public Task<User?> GetUserByIdAsync(Guid Id);
   public Task<Address?> UpdateAddressPatchAsync(Guid userId, JsonPatchDocument<Address> updatesFromUserDocument);
   //public Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, JsonPatchDocument updatesFromUserDocument);
   public Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
   public Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
}
