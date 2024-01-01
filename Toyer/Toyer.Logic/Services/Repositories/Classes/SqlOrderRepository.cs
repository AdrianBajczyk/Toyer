using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlOrderRepository : IOrderRepository
{
    private readonly ToyerDbContext _dbContext;

    public SqlOrderRepository(ToyerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> CreateNewOrderAsync(Order order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }
    public async Task<ICollection<Order>?> GetAllOrdersAsync() => await _dbContext.Orders.ToListAsync();
    public async Task<Order?> GetOrderByIdAsync(int orderId) => await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
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
