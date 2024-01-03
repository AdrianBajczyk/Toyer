using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceTypeRepository : IDeviceTypeRepository
{

    private readonly ToyerDbContext _dbContext;

    public SqlDeviceTypeRepository(ToyerDbContext dbContext) => _dbContext = dbContext;

    public async Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice)
    {
        await _dbContext.DeviceTypes.AddAsync(newDevice);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public async Task<DeviceType?> DeleteDeviceTypeAsync(int id)
    {
        var deviceToDelete = await GetDeviceTypeByIdAsync(id);

        if (deviceToDelete == null) return null;

        _dbContext.DeviceTypes.Remove(deviceToDelete);
        await _dbContext.SaveChangesAsync();

        return deviceToDelete;
    }

    public async Task<ICollection<DeviceType>?> GetAllDeviceTypesAsync() => await _dbContext.DeviceTypes.ToListAsync();

    public async Task<DeviceType?> GetDeviceTypeByIdAsync(int id) => await _dbContext.DeviceTypes.Include(dt => dt.Orders).FirstOrDefaultAsync(d => d.Id == id);

    public async Task<DeviceType?> UpdateDeviceTypeAsync(int id, DeviceType deviceTypeUpdateInfo)
    {
        var deviceToUpdate = await GetDeviceTypeByIdAsync(id);

        if (deviceToUpdate == null) return null;
        if (deviceToUpdate.Name != null) deviceToUpdate.Name = deviceTypeUpdateInfo.Name;
        if (deviceToUpdate.Description != null) deviceToUpdate.Description = deviceTypeUpdateInfo.Description;

        await _dbContext.SaveChangesAsync();

        return deviceToUpdate;
    }
}
