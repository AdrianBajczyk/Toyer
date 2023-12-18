using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories;

public class SqlDeviceTypeRepository : IDeviceTypeRepository
{

    private readonly ToyerDbContext _dbContext;

    public SqlDeviceTypeRepository(ToyerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<DeviceType?> CreateDeviceTypeAsync(DeviceType newDevice)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> DeleteDeviceTypeAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetAllDeviceTypesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetDeviceTypeByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> UpdateDeviceTypeAsync(User deviceTypeUpdateInfo, Guid Id)
    {
        throw new NotImplementedException();
    }
}
