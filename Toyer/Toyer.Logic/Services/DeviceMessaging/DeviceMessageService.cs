using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Toyer.Logic.Services.DeviceMessaging;

public class DeviceMessageService : IDeviceMessageService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public DeviceMessageService(ILogger logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public async Task SendCloudToDeviceMessageAsync(Guid targetDevice, string message)
    {
        ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetConnectionString("AZURE_IOT_HUB_SERVICE_CONNECTIONSTRING"));
        var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
        await serviceClient.SendAsync(targetDevice.ToString(), commandMessage);
    }


}
