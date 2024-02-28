using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Toyer.Logic.Services.DeviceMessaging;

public class DeviceMessageService : IDeviceMessageService
{
    private readonly ILogger<DeviceMessageService> _logger;
    private readonly SecretClient _client;

    public DeviceMessageService(ILogger<DeviceMessageService> logger, SecretClient client)
    {
        _logger = logger;
        _client = client;
    }
    public async Task SendCloudToDeviceMessageAsync(string targetDeviceId, string message)
    {

            var azureIotHubServiceConnectionstring = _client.GetSecret("AzureIotHubServiceConnectionstring").Value.Value.ToString();

        await Console.Out.WriteLineAsync(_client.GetSecret("AzureIotHubServiceConnectionstring").Value.Value.ToString());

        ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(azureIotHubServiceConnectionstring);
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, commandMessage);

            //serviceClient.GetFileNotificationReceiver need to implement D2C2Backend message about success

    }


}
