
namespace Toyer.Logic.Services.DeviceMessaging;

public interface IDeviceMessageService
{
    Task SendCloudToDeviceMessageAsync(Guid targetDevice, string message);
}