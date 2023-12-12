using Microsoft.AspNetCore.JsonPatch;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   public Task<User?> CreateNewUserAsync(User newUser, PersonalInfo newPersonalInfo, AddressDto newAddress);
   public Task<User?> GetUserByIdAsync(Guid Id);
   public Task<AddressDto> UpdateAddressPatchAsync(Guid userId, JsonPatchDocument<AddressDto> updatesFromUserDocument);
   public Task<AddressDto> UpdatePersonalInfoPatchAsync(Guid userId, JsonPatchDocument<UserPersonalInfoDto> updatesFromUserDocument);
   public Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
   public Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
}
