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

    public Task<DeviceType?> CreateAsync(DeviceType newDevice)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceType?> UpdateAsync(User deviceTypeUpdateInfo, Guid Id)
    {
        throw new NotImplementedException();
    }
}
