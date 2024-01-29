using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceTypeRepository(ToyerDbContext dbContext) : IDeviceTypeRepository
{

    private readonly ToyerDbContext _dbContext = dbContext;

    public async Task<ICollection<DeviceType>> GetAllDeviceTypesAsync() 
    {
        var deviceTypes = await _dbContext.DeviceTypes.ToListAsync();

        return deviceTypes.IsNullOrEmpty()
            ? throw new DeviceTypesTableEmptyException()
            : deviceTypes;
    }
    public async Task<DeviceType> GetDeviceTypeByIdAsync(int deviceTypeId) 
    {
        return await _dbContext.DeviceTypes
            .Include(dt => dt.Orders)
            .SingleOrDefaultAsync(d => d.Id == deviceTypeId)
            ?? throw new DeviceTypeNotFoundException(deviceTypeId);
    } 

    public async Task<DeviceType> CreateDeviceTypeAsync(DeviceType newDevice)
    {
        await _dbContext.DeviceTypes.AddAsync(newDevice);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public async Task DeleteDeviceTypeAsync(int deviceTypeId)
    {
        var deviceToDelete = await GetDeviceTypeByIdAsync(deviceTypeId) 
            ?? throw new DeviceTypeNotFoundException(deviceTypeId);

        _dbContext.DeviceTypes.Remove(deviceToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDeviceTypeAsync(int deviceTypeId, DeviceType deviceTypeUpdateInfo)
    {
        var deviceToUpdate = await GetDeviceTypeByIdAsync(deviceTypeId) 
            ?? throw new DeviceTypeNotFoundException(deviceTypeId);

        if (deviceToUpdate.Name != null) deviceToUpdate.Name = deviceTypeUpdateInfo.Name;
        if (deviceToUpdate.Description != null) deviceToUpdate.Description = deviceTypeUpdateInfo.Description;

        await _dbContext.SaveChangesAsync();
    }
}
