using Toyer.Data.Entities;
using Toyer.Logic.Responses;
using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<Device?> CreateNewDeviceAsync(int deviceTypeId);
    Task<Device?> DeleteDeviceByIdAsync(string deviceId);
    Task<Device?> GetDeviceByIdAsync(string deviceId);
    Task<CustomResponse> SendOrderToDevice(string deviceId, int orderId);
    Task<Device?> UpdateDeviceNameAsync(string deviceId ,string nameUpdateDto);

}
