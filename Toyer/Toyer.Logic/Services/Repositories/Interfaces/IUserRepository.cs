using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

interface IUserRepository
{
    Task<UserPresentShortDto?> CreateAsync(UserCreateDto userCreationInfo);
    Task<UserPresentLongDto?> UpdateAsync(UserCreateDto userCreationInfo);
    Task<UserPresentShortDto?> GetSketchyAsync(string login);
    Task<UserPresentLongDto?> GetExtendedAsync(string login);
    Task<UserPresentDevices?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin);
    Task<UserPresentDevices?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin);
}
