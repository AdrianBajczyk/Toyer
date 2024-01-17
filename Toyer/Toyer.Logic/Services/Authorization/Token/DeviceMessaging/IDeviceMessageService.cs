namespace Toyer.Logic.Services.Authorization.Token.DeviceMessaging;

public interface IDeviceMessageService
{
    Task SendCloudToDeviceMessageAsync(string targetDevice, string message);
}