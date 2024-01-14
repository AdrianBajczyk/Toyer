using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.Device;
using Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;
using Toyer.Logic.Exceptions.FailResponses.Derived.Order;
using Toyer.Logic.Services.DeviceMessaging;
using Toyer.Logic.Services.DeviceProvisioningService;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceRepository : IDeviceRepository
{
    private readonly ToyerDbContext _dbContext;
    private readonly IDeviceTypeRepository _deviceTypeRepository;
    private readonly IDpsClient _dpsClient;
    private readonly IDeviceMessageService _messageService;
    private readonly IOrderRepository _orderRepository;

    public SqlDeviceRepository(
        ToyerDbContext dbContext, 
        IDeviceTypeRepository deviceTypeRepository, 
        IDpsClient dpsClient, 
        IDeviceMessageService messageService,
        IOrderRepository orderRepository)
    {
        _dbContext = dbContext;
        _deviceTypeRepository = deviceTypeRepository;
        _dpsClient = dpsClient;
        _messageService = messageService;
        _orderRepository = orderRepository;
    }
    public async Task<Device> CreateNewDeviceAsync(int deviceTypeId)
    {
        var deviceTypeToAssign = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId) 
            ?? throw new DeviceTypeNotFoundException(deviceTypeId);

        var newDevice = new Device
        {
            Name = deviceTypeToAssign.Name + "_Device",
            DeviceType = deviceTypeToAssign
        };

        await _dbContext.Devices.AddAsync(newDevice);

        await _dpsClient.RegisterDevice(newDevice.Id.ToString(), newDevice.DeviceType.Name);
        await _dbContext.SaveChangesAsync();

        return newDevice;
    }


    public async Task<Device> GetDeviceByIdAsync(string deviceId)
    {
        var device = await _dbContext.Devices
            .Include(d => d.DeviceType)
            .ThenInclude(dt => dt.Orders)
            .FirstOrDefaultAsync(d => d.Id.ToString() == deviceId);

        return device 
            ?? throw new DeviceNotFoundException(deviceId);
    }


    public async Task SendOrderToDevice(string deviceId, int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) 
            ?? throw new OrderNotFoundException(orderId);

        _ = await GetDeviceByIdAsync(deviceId)
            ?? throw new DeviceNotFoundException(deviceId);

        await _messageService.SendCloudToDeviceMessageAsync(deviceId, order.MessageBody);
    }


    public async Task UpdateDeviceNameAsync(string deviceId, string nameUpdateDto)
    {
        var device = await GetDeviceByIdAsync(deviceId)
            ?? throw new DeviceNotFoundException(deviceId);

        device.Name = nameUpdateDto;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDeviceByIdAsync(string deviceId)
    {
        var deviceToDelete = await GetDeviceByIdAsync(deviceId)
            ?? throw new DeviceNotFoundException(deviceId);

        _dbContext.Devices.Remove(deviceToDelete);
        await _dbContext.SaveChangesAsync();
    }
}
