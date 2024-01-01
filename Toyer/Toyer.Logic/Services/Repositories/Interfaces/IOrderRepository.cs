using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.API.Controllers;

public interface IOrderRepository
{
    Task<Order> CreateNewOrderAsync(Order order);
    Task<ICollection<Order>?> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> UpdateOrderByIdAsync(int orderId, OrderCreateDto orderUpdates);
}