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
    public async Task<Address?> UpdateAddressPatchAsync(Guid userId, Address updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return null;

        var addressToUpdate = userToUpdate.PersonalInfo.Address;

        if (updatesFromUser.State != null) addressToUpdate.State = updatesFromUser.State;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.City != null) addressToUpdate.City = updatesFromUser.City;
        if (updatesFromUser.Country != null) addressToUpdate.Country = updatesFromUser.Country;
        if (updatesFromUser.PostalCode != null) addressToUpdate.PostalCode = updatesFromUser.PostalCode;
       
        await _dbContext.SaveChangesAsync();

        return addressToUpdate;
    }
    public async Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, PersonalInfo updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return null;

        var personalInfoToUpdate = userToUpdate.PersonalInfo;

        if (updatesFromUser.Name != null) updatesFromUser.Name = personalInfoToUpdate.Name;
        if (updatesFromUser.Surname != null) updatesFromUser.Surname = personalInfoToUpdate.Surname;
        if (updatesFromUser.PhoneNumber != null) updatesFromUser.PhoneNumber = personalInfoToUpdate.PhoneNumber;
        if (updatesFromUser.Email != null) updatesFromUser.Email = personalInfoToUpdate.Email;
        if (updatesFromUser.BirthDate != null) updatesFromUser.BirthDate = personalInfoToUpdate.BirthDate;

        await _dbContext.SaveChangesAsync();

        return personalInfoToUpdate;
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
