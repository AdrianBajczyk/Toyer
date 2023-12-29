﻿using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceTypeRepository
{
    public Task<ICollection<DeviceType>?> GetAllDeviceTypesAsync();
    public Task<DeviceType?> GetDeviceTypeByIdAsync(int id);
    public Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice);
    public Task<DeviceType?> DeleteDeviceTypeAsync(int id);
    public Task<DeviceType?> UpdateDeviceTypeAsync(int id, DeviceType deviceTypeUpdateInfo);
}
