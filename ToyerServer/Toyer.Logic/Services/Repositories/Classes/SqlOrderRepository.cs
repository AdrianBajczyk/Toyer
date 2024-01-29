using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;
using Toyer.Logic.Exceptions.FailResponses.Derived.Order;
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

    public async Task AssignOrderToDeviceTypesAsync(int orderId, IEnumerable<int> deviceTypeIds)
    {
        var orderToAssign = await GetOrderByIdAsync(orderId);
        if (orderToAssign == null) throw new OrderNotFoundException(orderId);

        var exisitingDeviceTypes = await _deviceTypeRepository.GetAllDeviceTypesAsync();

        var nonExistentDeviceTypeIds = CheckForNonExisitngIds(deviceTypeIds, exisitingDeviceTypes);
        if (nonExistentDeviceTypeIds != null) throw new DeviceTypesNotFoundException(nonExistentDeviceTypeIds);
        

        var duplicatedIds = CheckForDuplicatesInDb(deviceTypeIds, orderToAssign);
        if (duplicatedIds != null ) throw new DeviceTypesHasGivenOrderException(duplicatedIds);

        PerformAssigment(deviceTypeIds, orderToAssign, exisitingDeviceTypes!);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<Order> CreateNewOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }
    public async Task<ICollection<Order>?> GetAllOrdersAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.DeviceTypes)
            .AsNoTracking()
            .ToListAsync()
            ?? throw new OrderTableEmptyException();
    }

    public async Task<Order> GetOrderByIdAsync(int orderId) 
    {
        return await _dbContext.Orders
            .Include(o => o.DeviceTypes)
            .SingleOrDefaultAsync(o => o.Id == orderId)
            ?? throw new OrderNotFoundException(orderId);
    }
    public async Task UpdateOrderByIdAsync(int orderId, OrderCreateDto orderUpdates)
    {
        var orderToUpdate = await GetOrderByIdAsync(orderId) 
            ?? throw new OrderNotFoundException(orderId);

        if (orderUpdates.Name != null) orderToUpdate.Name = orderUpdates.Name;
        if (orderUpdates.Description != null) orderToUpdate.Description = orderUpdates.Description;
        if (orderUpdates.MessageBody != null) orderToUpdate.MessageBody = orderUpdates.MessageBody;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderByIdAsync(int orderId)
    {
        var orderToDelete = await GetOrderByIdAsync(orderId) 
            ?? throw new OrderNotFoundException(orderId);

        _dbContext.Orders.Remove(orderToDelete);
        await _dbContext.SaveChangesAsync();

    }

    private static void PerformAssigment(IEnumerable<int> deviceTypeIds, Order orderToAssign, ICollection<DeviceType> deviceTypes)
    {
        foreach (int deviceTypeId in deviceTypeIds)
        {
            var deviceType = deviceTypes!.First(dt => dt.Id == deviceTypeId);
            orderToAssign.DeviceTypes.Add(deviceType!);
        }
    }
    private static IEnumerable<int>? CheckForNonExisitngIds(IEnumerable<int> deviceTypeIds, ICollection<DeviceType>? deviceTypes)
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
    private static IEnumerable<int>? CheckForDuplicatesInDb(IEnumerable<int> deviceTypeIds, Order orderToAssign)
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
