using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   public Task<User?> CreateAsync(User newUser, PersonalInfo newPersonalInfo, Address newAddress);
   public Task<User?> GetByIdAsync(Guid Id);
   public Task<User?> UpdateAsync(User userUpdateInfo, Guid Id);
   public Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
   public Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
}
