using Toyer.Data.Entities;
using Toyer.Logic.Responses;
using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<Device?> CreateNewDeviceAsync(int deviceTypeId);
    Task<Device?> DeleteDeviceByIdAsync(Guid deviceId);
    Task<Device?> GetDeviceByIdAsync(Guid deviceId);
    Task<CustomResponse> SendOrderToDevice(Guid deviceId, int orderId);
    Task<Device?> UpdateDeviceNameAsync(Guid deviceId ,string nameUpdateDto);

}
