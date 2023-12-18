using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceTypeRepository
{
    public Task<DeviceType?> GetAllDeviceTypesAsync();
    public Task<DeviceType?> GetDeviceTypeByIdAsync(Guid Id);
    public Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice);
    public Task<DeviceType?> DeleteDeviceTypeAsync(Guid Id);
    public Task<DeviceType?> UpdateDeviceTypeAsync(User deviceTypeUpdateInfo, Guid Id);
}
