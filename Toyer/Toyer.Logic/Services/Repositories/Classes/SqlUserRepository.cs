using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlUserRepository : IUserRepository

{
    private readonly ToyerDbContext _dbContext;
    private readonly IDeviceRepository _deviceRepository;

    public SqlUserRepository(ToyerDbContext dbContext, IDeviceRepository deviceRepository)
    {
        _dbContext = dbContext;
        _deviceRepository = deviceRepository;
    }
    public async Task<User?> GetUserByIdAsync(Guid Id)
    {
        return await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .Include(u => u.Devices)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<List<User>?> GetUsersAsync()
    {
        return await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .Include(u => u.Devices)
            .ToListAsync();

    }
    public async Task<User?> CreateNewUserAsync(User newUser)
    {
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return newUser;
    }
    public async Task<Address?> UpdateAddressPatchAsync(Guid userId, Address updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return null;

        var addressToUpdate = userToUpdate.PersonalInfo!.Address!;

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

        var personalInfoToUpdate = userToUpdate.PersonalInfo!;

        if (updatesFromUser.Name != null) personalInfoToUpdate.Name = updatesFromUser.Name;
        if (updatesFromUser.Surname != null) personalInfoToUpdate.Surname = updatesFromUser.Surname;
        if (updatesFromUser.PhoneNumber != null) personalInfoToUpdate.PhoneNumber = updatesFromUser.PhoneNumber;
        if (updatesFromUser.Email != null) personalInfoToUpdate.Email = updatesFromUser.Email;
        if (updatesFromUser.BirthDate != default) personalInfoToUpdate.BirthDate = updatesFromUser.BirthDate;

        await _dbContext.SaveChangesAsync();

        return personalInfoToUpdate;
    }
    public async Task<User?> DeleteUserAsync(Guid Id)
    {
        var userToDelete = await GetUserByIdAsync(Id);

        if (userToDelete == null) return null;

        _dbContext.Users.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();

        return userToDelete;
    }

    public async Task<CustomResponse> AssignDeviceToUserAsync(Guid userId, Guid deviceId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) return new CustomResponse() { Message = "User not found", StatusCode = 404 };

        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
        if (device == null) return new CustomResponse() { Message = "Device not found", StatusCode = 404 };

        if (await IsAssignedToAnyUser(deviceId)) return new CustomResponse() { Message = "Device has been already assigned.", StatusCode = 400 };

        user.Devices.Add(device);
        await _dbContext.SaveChangesAsync();

        return new CustomResponse() { Message = "Ok", StatusCode = 200 };
    }

    private async Task<bool> IsAssignedToAnyUser(Guid deviceId)
    {
        var users = await GetUsersAsync();
        return users?.Any(user => user.Devices.Any(device => device.Id == deviceId)) ?? false;
    }
}
