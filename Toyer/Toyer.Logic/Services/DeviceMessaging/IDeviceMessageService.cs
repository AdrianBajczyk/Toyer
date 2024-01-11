
namespace Toyer.Logic.Services.DeviceMessaging;

public interface IDeviceMessageService
{
    Task SendCloudToDeviceMessageAsync(string targetDevice, string message);
}