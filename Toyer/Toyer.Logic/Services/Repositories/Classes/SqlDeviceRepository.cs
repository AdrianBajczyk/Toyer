using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceRepository : IDeviceRepository
{
    private readonly ToyerDbContext _dbContext;
    private readonly IDeviceTypeRepository _deviceTypeRepository;

    public SqlDeviceRepository(ToyerDbContext dbContext, IDeviceTypeRepository deviceTypeRepository)
    {
        _dbContext = dbContext;
        _deviceTypeRepository = deviceTypeRepository;
    }
    public async Task<Device?> CreateNewDeviceAsync(int deviceTypeId)
    {
        var deviceTypeToAssign = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId);
        if (deviceTypeToAssign == null) return null;

        var newDevice = new Device { Name = deviceTypeToAssign.Name + "_Device" };
        newDevice.DeviceType = deviceTypeToAssign;

        await _dbContext.Devices.AddAsync(newDevice);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public Task<Device> UpdateDeviceName(DeviceNameUpdateDto nameUpdateDto)
    {
        throw new NotImplementedException();
    }


}
