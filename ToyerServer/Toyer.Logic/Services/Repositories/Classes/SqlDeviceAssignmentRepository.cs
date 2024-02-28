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

        if (_toyerDbContext.UsersDevices.Any(r => r.Devices.Contains(device))) 
            throw new DeviceIsAssignedException(deviceId);

        var relation =  await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);
        if (relation == null) 
        { 
            await CreateRelationId(userId);
            relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);
        }

        relation!.Devices.Add(device);
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

        if (!relation.Devices.Contains(device))
            throw new UserHasNoDeviceException(userId, deviceId);

        relation.Devices.Remove(device);
        await _toyerDbContext.SaveChangesAsync();
    }

    public async Task DeleteDeviceAsync(string deviceId)
    {
        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId)
            ?? throw new DeviceNotFoundException(deviceId);

        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.Devices.Contains(device));
        relation?.Devices.Remove(device);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);
        if (relation != null) _toyerDbContext.UsersDevices.Remove(relation);
    }

    public async Task<string> GetUserIdByAssignedDeviceId(string deviceId)
    {
        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId)
            ?? throw new DeviceNotFoundException(deviceId);

        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.Devices.Contains(device)) 
            ?? throw new DeviceIsUnassignedException(deviceId);

        return relation.UserId;
    }

    private async Task CreateRelationId(string userId)
    {
        await _toyerDbContext.UsersDevices.AddAsync(new UserDevices() { UserId = userId });
        await _toyerDbContext.SaveChangesAsync();
    }

    public async Task<UserDevices> GetAllDevicesAssignedToUser(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId)
            ?? throw new UserNotFoundException(userId);

        var userDevices = await _toyerDbContext.UsersDevices
        .Include(ud => ud.Devices) 
        .ThenInclude(d => d.DeviceType) 
        .ThenInclude(dt => dt.Orders)                          
        .SingleOrDefaultAsync(ud => ud.UserId == userId)
        ?? throw new UserHasNoDevicesException(userId);

        await Console.Out.WriteLineAsync("USER DEVICES");
        await Console.Out.WriteLineAsync(userDevices.Devices.ToString());

        return userDevices;
    }
}
