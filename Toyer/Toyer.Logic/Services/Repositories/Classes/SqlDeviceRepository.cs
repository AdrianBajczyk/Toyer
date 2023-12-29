using Toyer.Data.Context;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceRepository : IDeviceRepository
{
    private readonly ToyerDbContext _dbContext;

    public SqlDeviceRepository(ToyerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<DeviceCreateDto> CreateNewDeviceAsync(DeviceCreateDto deviceCreateInfo)
    {
        throw new NotImplementedException();
    }

    public Task<DevicePresentDto> UpdateDeviceName(DeviceNameUpdateDto nameUpdateDto)
    {
        throw new NotImplementedException();
    }


}
