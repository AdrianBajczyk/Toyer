using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Entities;

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
}
