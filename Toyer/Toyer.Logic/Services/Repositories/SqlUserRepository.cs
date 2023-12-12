using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public async Task<User?> GetUserByIdAsync(Guid Id)
    {
        return await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }
    public async Task<User?> CreateNewUserAsync(User newUser, PersonalInfo newPersonalInfo, Address newAddress)
    {
        newPersonalInfo.Address = newAddress;
        newUser.PersonalInfo = newPersonalInfo;
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return newUser;
    }
    public async Task<Address?> UpdateAddressPatchAsync(Guid userId, JsonPatchDocument<Address> updatesFromUserDocument)
    {
        var userData = await GetUserByIdAsync(userId);

        if (userData == null) return null;

        var addressToUpdate = userData.PersonalInfo.Address;


        updatesFromUserDocument.ApplyTo(addressToUpdate);

        await _dbContext.SaveChangesAsync();

        return addressToUpdate;
    }

    //public async Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, JsonPatchDocument updatesFromUserDocument)
    //{
    //    var userData = await GetUserByIdAsync(userId);

    //    if (userData == null) return null;

    //    var personalInfoToUpdate = userData.PersonalInfo;

    //    updatesFromUserDocument.ApplyTo(personalInfoToUpdate);

    //    await _dbContext.SaveChangesAsync();

    //    throw new NotImplementedException();
    //}

    public async Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin)
    {
        throw new NotImplementedException();
    }

}
