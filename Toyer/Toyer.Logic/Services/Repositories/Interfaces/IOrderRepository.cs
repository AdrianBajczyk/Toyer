using Toyer.Data.Entities;

namespace Toyer.API.Controllers;

public interface IOrderRepository
{
    Task<Order> CreateNewOrderAsync(Order order);
}