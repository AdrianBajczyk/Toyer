using Microsoft.AspNetCore.Http;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Services.Repositories.Interfaces
{
    public interface IDeviceAssignRepository
    {
        Task<CustomResponse> AssignDeviceToUserAsync(string deviceId, string userId);
    }
}