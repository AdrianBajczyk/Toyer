using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;
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
    public async Task<Device?> CreateNewDeviceAsync(int deviceTypeId)
    {
        var deviceTypeToAssign = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId);
        if (deviceTypeToAssign == null) return null;

        var newDevice = new Device { Name = deviceTypeToAssign.Name + "_Device" };
        newDevice.DeviceType = deviceTypeToAssign;

        await _dbContext.Devices.AddAsync(newDevice);

        await _dpsClient.RegisterDevice(newDevice.Id, newDevice.DeviceType.Name);
        //await _dbContext.SaveChangesAsync();

        return newDevice;
    }

    public async Task<Device?> DeleteDeviceByIdAsync(Guid deviceId)
    {
        var deviceToDelete = await GetDeviceByIdAsync(deviceId);
        if (deviceToDelete == null) return null;

        _dbContext.Devices.Remove(deviceToDelete);
        await _dbContext.SaveChangesAsync();

        return deviceToDelete;
    }

    public async Task<Device?> GetDeviceByIdAsync(Guid deviceId) => await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);

    public async Task<CustomResponse> SendOrderToDevice(Guid deviceId, int orderId)
    {
        var orderMessage = await _orderRepository.GetOrderByIdAsync(orderId);
        if (orderMessage == null) return new CustomResponse() { Message = "Order not found.", StatusCode = "404" };

        var device = await GetDeviceByIdAsync(deviceId);
        if (device == null) return new CustomResponse() { Message = "Device nont found.", StatusCode = "404" };

        await _messageService.SendCloudToDeviceMessageAsync(deviceId, orderMessage.MessageBody);
        return new CustomResponse() { Message = "Order send.", StatusCode = "200" };
    }


    public async Task<Device?> UpdateDeviceNameAsync(Guid deviceId, string nameUpdateDto)
    {
        var device = await GetDeviceByIdAsync(deviceId);
        if (device == null) return null;

        device.Name = nameUpdateDto;
        await _dbContext.SaveChangesAsync();

        return device;
    }
}
