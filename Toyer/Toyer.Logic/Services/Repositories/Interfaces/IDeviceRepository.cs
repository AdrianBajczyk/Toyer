using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IDeviceRepository
{
   public Task<Device?> CreateNewDeviceAsync(int deviceTypeId);
   public Task<Device> UpdateDeviceName(DeviceNameUpdateDto nameUpdateDto);

}
