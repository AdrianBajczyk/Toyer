
namespace Toyer.Logic.Services.DeviceProvisioningService
{
    public interface IDpsClient
    {
        Task RegisterDevice(Guid id, string subsectionKeyName);
    }
}