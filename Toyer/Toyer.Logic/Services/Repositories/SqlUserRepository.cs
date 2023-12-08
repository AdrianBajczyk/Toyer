using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories;

public class SqlUserRepository : IUserRepository
{
    private readonly ToyerDbContext _dbContext;

    public SqlUserRepository(ToyerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User?> GetAsync(Guid Id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
    }
    public async Task<User?> CreateAsync(User userCreationInfo)
    {
        await _dbContext.Users.AddAsync(userCreationInfo);
        await _dbContext.SaveChangesAsync();

        return userCreationInfo;
    }
    public async Task<User?> UpdateAsync(User userUpdateInfo, Guid Id)
    {
        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);

        if (userToUpdate != null)
        {

        }
    }

    public async Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserLogin userLogin)
    {
        throw new NotImplementedException();
    }

}
