using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceTypeRepository
{
    public Task<DeviceType?> GetAllAsync();
    public Task<DeviceType?> GetByIdAsync(Guid Id);
    public Task<DeviceType?> CreateAsync(DeviceType newDevice);
    public Task<DeviceType?> DeleteAsync(Guid Id);
    public Task<DeviceType?> UpdateAsync(User deviceTypeUpdateInfo, Guid Id);
}
