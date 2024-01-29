
namespace Toyer.Logic.Services.DeviceProvisioningService
{
    public interface IDpsClient
    {
        Task RegisterDevice(string id, string subsectionKeyName);
    }
}