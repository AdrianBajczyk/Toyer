using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceTypeRepository
{
    public Task<ICollection<DeviceType>?> GetAllDeviceTypesAsync();
    public Task<DeviceType?> GetDeviceTypeByIdAsync(int Id);
    public Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice);
    public Task<DeviceType?> DeleteDeviceTypeAsync(int Id);
    public Task<DeviceType?> UpdateDeviceTypeAsync(DeviceType deviceTypeUpdateInfo, int Id);
}
