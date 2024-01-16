using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.Device;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceAssignmentRepository(IUserRepository userRepository, IDeviceRepository deviceRepository, ToyerDbContext toyerDbContext) : IDeviceAssignmentRepository
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    private readonly ToyerDbContext _toyerDbContext = toyerDbContext;

    public async Task AssignDeviceToUserAsync(string deviceId, string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId) 
            ?? throw new DeviceNotFoundException(deviceId);

        if (_toyerDbContext.UsersDevices.Any(r => r.DevicesIds.Contains(deviceId))) 
            throw new DeviceIsAssignedException(deviceId);

        var relation =  await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);

        if (relation == null) await CreateRelationId(userId);

        relation!.DevicesIds.Add(deviceId);
        await _toyerDbContext.SaveChangesAsync();
    }

    public async Task UnassignDeviceFromUserAsync(string deviceId, string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId) 
            ?? throw new DeviceNotFoundException(deviceId);

        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId)
            ?? throw new UserHasNoDeviceException(userId, deviceId);

        if (!relation.DevicesIds.Contains(deviceId))
            throw new UserHasNoDeviceException(userId, deviceId);

        relation.DevicesIds.Remove(deviceId);
        await _toyerDbContext.SaveChangesAsync();
    }

    public async Task DeleteDeviceAsync(string deviceId)
    {
        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.DevicesIds.Contains(deviceId));
        if (relation != null) relation.DevicesIds.Remove(deviceId);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);
        if (relation != null) _toyerDbContext.UsersDevices.Remove(relation);
    }

    private async Task CreateRelationId(string userId)
    {
        await _toyerDbContext.UsersDevices.AddAsync(new UserDevices() { UserId = userId });
        await _toyerDbContext.SaveChangesAsync();
    }
}
