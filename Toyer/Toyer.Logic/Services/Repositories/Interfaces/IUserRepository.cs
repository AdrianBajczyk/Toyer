using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

interface IUserRepository
{
    Task<User?> CreateAsync(User userCreationInfo);
    Task<User?> GetAsync(Guid Id);
    Task<User?> UpdateAsync(User userUpdateInfo, Guid Id);
    Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin);
    Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin);
}
