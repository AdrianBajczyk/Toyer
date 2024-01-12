using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlDeviceAssignRepository : IDeviceAssignRepository
{
    private readonly IUserRepository _userRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly ToyerDbContext _toyerDbContext;

    public SqlDeviceAssignRepository(IUserRepository userRepository, IDeviceRepository deviceRepository, ToyerDbContext toyerDbContext)
    {
        _userRepository = userRepository;
        _deviceRepository = deviceRepository;
        _toyerDbContext = toyerDbContext;
    }

    public async Task<CustomResponse> AssignDeviceToUserAsync(string deviceId, string userId)
    {

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return new CustomResponse { Message = "User not found.", StatusCode = "404" };

        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
        if (device == null) return new CustomResponse { Message = "Device not found.", StatusCode = "404" };

        if (_toyerDbContext.UsersDevices.Any(r => r.DevicesIds.Contains(deviceId))) 
            return new CustomResponse { Message = "Device is in assigned state. To change user, first unassign device from its current account.", StatusCode = "400" };

        var relation =  await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);

        if (relation == null) await CreateRelationId(userId);

        relation!.DevicesIds.Add(deviceId);
        await _toyerDbContext.SaveChangesAsync();

        return new CustomResponse { Message = "Device assigned.", StatusCode = "200" };
    }

    public async Task<CustomResponse> UnassignDeviceFromUserAsync(string deviceId, string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return new CustomResponse { Message = "User not found.", StatusCode = "404" };

        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
        if (device == null) return new CustomResponse { Message = "Device not found.", StatusCode = "404" };

        var relation = await _toyerDbContext.UsersDevices.FirstOrDefaultAsync(r => r.UserId == userId);
        if (relation == null) return new CustomResponse { Message = "User has no specified device.", StatusCode = "404" };

        if (!relation.DevicesIds.Contains(deviceId)) return new CustomResponse { Message = "User has no specified device.", StatusCode = "404" };

        relation.DevicesIds.Remove(deviceId);
        await _toyerDbContext.SaveChangesAsync();

        return new CustomResponse { Message = "Device unassigned.", StatusCode = "200" };
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
