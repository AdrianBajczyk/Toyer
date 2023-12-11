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
    public async Task<User?> GetByIdAsync(Guid Id)
    {
        return await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }
    public async Task<User?> CreateAsync(User newUser, PersonalInfo newPersonalInfo, Address newAddress)
    {
        newPersonalInfo.Address = newAddress;
        newUser.PersonalInfo = newPersonalInfo;
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return newUser;
    }
    public async Task<User?> UpdateAsync(User userUpdateInfo, Guid Id)
    {
        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);

        if (userToUpdate == null) return null;

        userToUpdate = userUpdateInfo;

        await _dbContext.SaveChangesAsync();

        return userToUpdate;

    }

    public async Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin)
    {
        throw new NotImplementedException();
    }

}
