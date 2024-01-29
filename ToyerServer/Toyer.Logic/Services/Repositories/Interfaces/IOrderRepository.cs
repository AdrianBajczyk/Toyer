using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Responses;

namespace Toyer.API.Controllers;

public interface IOrderRepository
{
    Task<Order> CreateNewOrderAsync(Order order);
    Task<ICollection<Order>?> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task AssignOrderToDeviceTypesAsync(int orderId, IEnumerable<int> deviceTypeListIds);
    Task UpdateOrderByIdAsync(int orderId, OrderCreateDto orderUpdates);
    Task DeleteOrderByIdAsync(int orderId);
}