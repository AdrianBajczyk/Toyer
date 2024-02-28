using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<Device> CreateNewDeviceAsync(int deviceTypeId);
    Task<Device> GetDeviceByIdAsync(string deviceId);
    Task SendOrderToDevice(string deviceId, int orderId);
    Task DeleteDeviceByIdAsync(string deviceId);
    Task UpdateDeviceNameAsync(string deviceId ,string nameUpdateDto);

}
