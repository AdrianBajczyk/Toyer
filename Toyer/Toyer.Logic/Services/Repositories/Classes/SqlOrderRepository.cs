using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlOrderRepository : IOrderRepository
{
    private readonly ToyerDbContext _dbContext;
    private readonly IDeviceTypeRepository _deviceTypeRepository;

    public SqlOrderRepository(ToyerDbContext dbContext, IDeviceTypeRepository deviceTypeRepository)
    {
        _dbContext = dbContext;
        _deviceTypeRepository = deviceTypeRepository;
    }

    public async Task<CustomResponse> AssignOrderToDeviceTypesAsync(int orderId, OrderAssignDto deviceTypesToAssign)
    {
        var orderToAssign = await GetOrderByIdAsync(orderId);
        if (orderToAssign == null) return new CustomResponse() { Message = "Order not found.", StatusCode = "404" };

        var deviceTypeIds = deviceTypesToAssign.DeviceTypeIds;
        var exisitingDeviceTypes = await _deviceTypeRepository.GetAllDeviceTypesAsync();

        var nonExistentDeviceTypeIds = CheckForNonExisitngIds(deviceTypeIds, exisitingDeviceTypes);
        if (nonExistentDeviceTypeIds != null)
        {
            return new CustomResponse() { Message = $"ID/s: [{string.Join(", ", nonExistentDeviceTypeIds)}] to be assigned not found", StatusCode = "404" };
        }

        var duplicatedIds = CheckForDuplicatesInDb(deviceTypeIds, orderToAssign);
        if (duplicatedIds != null )
        {
            return new CustomResponse() { Message = $"ID/s: [{string.Join(", ", duplicatedIds)}] has/have a member of given order", StatusCode = "400" };
        }

        PerformAssigment(deviceTypeIds, orderToAssign, exisitingDeviceTypes!);
        await _dbContext.SaveChangesAsync();

        return new CustomResponse() { Message = $"Order has been assigned to: [{string.Join(",", deviceTypeIds)}] device type/s id/s", StatusCode = "200" };
    }

    public async Task<Order> CreateNewOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }
    public async Task<ICollection<Order>?> GetAllOrdersAsync() => await _dbContext.Orders.Include(o => o.DeviceTypes).ToListAsync();
    public async Task<Order?> GetOrderByIdAsync(int orderId) => await _dbContext.Orders.Include(o => o.DeviceTypes).FirstOrDefaultAsync(o => o.Id == orderId);
    public async Task<Order?> UpdateOrderByIdAsync(int orderId, OrderCreateDto orderUpdates)
    {
        var orderToUpdate = await GetOrderByIdAsync(orderId);

        if (orderToUpdate == null) return null;
        if (orderUpdates.Name != null) orderToUpdate.Name = orderUpdates.Name;
        if (orderUpdates.Description != null) orderToUpdate.Description = orderUpdates.Description;
        if (orderUpdates.MessageBody != null) orderToUpdate.MessageBody = orderUpdates.MessageBody;

        await _dbContext.SaveChangesAsync();

        return orderToUpdate;
    }

    public async Task<Order?> DeleteOrderByIdAsync(int orderId)
    {
        var orderToDelete = await GetOrderByIdAsync(orderId);
        if (orderToDelete == null) return null;

        _dbContext.Orders.Remove(orderToDelete);
        await _dbContext.SaveChangesAsync();

        return orderToDelete;
    }

    private static void PerformAssigment(List<int> deviceTypeIds, Order orderToAssign, ICollection<DeviceType> deviceTypes)
    {
        foreach (var deviceTypeId in deviceTypeIds)
        {
            var deviceType = deviceTypes!.First(dt => dt.Id == deviceTypeId);
            orderToAssign.DeviceTypes.Add(deviceType!);
        }
    }
    private static List<int>? CheckForNonExisitngIds(List<int> deviceTypeIds, ICollection<DeviceType>? deviceTypes)
    {
        if (deviceTypes == null) return deviceTypeIds;

        var nonExistentDeviceTypeIds = new List<int>();

        foreach (var deviceTypeId in deviceTypeIds)
        {
            var deviceType = deviceTypes.FirstOrDefault(dt => dt.Id == deviceTypeId);
            if (deviceType == null) nonExistentDeviceTypeIds.Add(deviceTypeId);
        }

        return nonExistentDeviceTypeIds.Count > 0 ? nonExistentDeviceTypeIds : null;
    }
    private static List<int>? CheckForDuplicatesInDb(List<int> deviceTypeIds, Order orderToAssign)
    {
        var duplicatedIds = new List<int>();

        foreach (var id in deviceTypeIds)
        {
            foreach (var deviceType in orderToAssign.DeviceTypes)
            {
                if (deviceType.Id == id) duplicatedIds.Add(id);
            }
        }

        return duplicatedIds.Count > 0 ? duplicatedIds : null;
    }
}
