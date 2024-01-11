using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Toyer.Logic.Services.DeviceMessaging;

public class DeviceMessageService : IDeviceMessageService
{
    private readonly ILogger<DeviceMessageService> _logger;
    private readonly IConfiguration _configuration;

    public DeviceMessageService(ILogger<DeviceMessageService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public async Task SendCloudToDeviceMessageAsync(string targetDeviceId, string message)
    {
        var azureIotHubServiceConnectionstring = _configuration["AzureIotHubServiceConnectionstring"];

        ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(azureIotHubServiceConnectionstring);
        var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
        await serviceClient.SendAsync(targetDeviceId, commandMessage);
        
    }


}
