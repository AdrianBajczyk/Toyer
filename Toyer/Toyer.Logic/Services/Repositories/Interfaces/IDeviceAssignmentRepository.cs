using Microsoft.AspNetCore.Http;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Services.Repositories.Interfaces
{
    public interface IDeviceAssignmentRepository
    {
        Task AssignDeviceToUserAsync(string deviceId, string userId);
        Task UnassignDeviceFromUserAsync(string deviceId, string userId);
        Task DeleteUserAsync(string userId);
        Task DeleteDeviceAsync(string deviceId);
        Task<string> GetUserIdByAssignedDeviceId(string deviceId);
    }
}