using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceTypeRepository : IDeviceTypeRepository
{

    private readonly ToyerDbContext _dbContext;

    public SqlDeviceTypeRepository(ToyerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice)
    {
        await _dbContext.AddAsync(newDevice);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public Task<DeviceType?> DeleteDeviceTypeAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetAllDeviceTypesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetDeviceTypeByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> UpdateDeviceTypeAsync(DeviceType deviceTypeUpdateInfo, Guid Id)
    {
        throw new NotImplementedException();
    }
}
