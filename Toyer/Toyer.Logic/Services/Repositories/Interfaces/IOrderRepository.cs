using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Services.Validations;

namespace Toyer.API.Controllers;

public interface IOrderRepository
{
    Task<AssignmentResult<Order, int>> AssignOrderToDeviceTypesAsync(int orderId, OrderAssignDto deviceTypeListIds);
    Task<Order> CreateNewOrderAsync(Order order);
    Task<ICollection<Order>?> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> UpdateOrderByIdAsync(int orderId, OrderCreateDto orderUpdates);
    Task<Order?> DeleteOrderByIdAsync(int orderId);
}