﻿using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Services.Repositories.Interfaces;
using Toyer.Logic.Services.Validations;

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

    public async Task<AssignmentResult<Order, int>> AssignOrderToDeviceTypesAsync(int orderId, OrderAssignDto deviceTypesToAssign)
    {
        var orderToAssign = await GetOrderByIdAsync(orderId);
        if (orderToAssign == null) return new AssignmentResult<Order, int>();

        var deviceTypeIds = deviceTypesToAssign.DeviceTypeIds;
        var exisitingDeviceTypes = await _deviceTypeRepository.GetAllDeviceTypesAsync();

        var nonExistentDeviceTypeIds = CheckForNonExisitngIdsInDb(deviceTypeIds, exisitingDeviceTypes);
        var duplicatedIds = CheckForDuplicatesInDb(deviceTypeIds, orderToAssign);
        if (duplicatedIds != null || nonExistentDeviceTypeIds != null) return new AssignmentResult<Order, int>(nonExistentDeviceTypeIds, duplicatedIds);

        PerformAssigment(deviceTypeIds, orderToAssign, exisitingDeviceTypes!);
        await _dbContext.SaveChangesAsync();

        return new AssignmentResult<Order, int>(orderToAssign);
    }

    private static void PerformAssigment(List<int> deviceTypeIds, Order orderToAssign, ICollection<DeviceType> deviceTypes)
    {
        foreach (var deviceTypeId in deviceTypeIds)
        {
            var deviceType = deviceTypes!.First(dt => dt.Id == deviceTypeId);
            orderToAssign.DeviceTypes.Add(deviceType!);
        }
    }
    private static List<int>? CheckForNonExisitngIdsInDb(List<int> deviceTypeIds, ICollection<DeviceType>? deviceTypes)
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

    

    public async Task<Order> CreateNewOrderAsync(Order order)
    {
        await _dbContext.AddAsync(order);
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
}
