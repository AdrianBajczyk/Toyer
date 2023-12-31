using Toyer.Data.Entities;

namespace Toyer.API.Controllers;

public interface IOrdersRepository
{
    Task<Order> CreateNewOrderAsync(Order order);
}