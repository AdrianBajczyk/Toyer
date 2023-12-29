using Microsoft.EntityFrameworkCore;
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
        await _dbContext.DeviceTypes.AddAsync(newDevice);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public Task<DeviceType?> DeleteDeviceTypeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<DeviceType>?> GetAllDeviceTypesAsync()
    {
        return await _dbContext.DeviceTypes.ToListAsync();
    }

    public async Task<DeviceType?> GetDeviceTypeByIdAsync(int id)
    {
        return await _dbContext.DeviceTypes.FirstOrDefaultAsync(d => d.Id == id);
    }

    public Task<DeviceType?> UpdateDeviceTypeAsync(DeviceType deviceTypeUpdateInfo, int id)
    {
        throw new NotImplementedException();
    }
}
