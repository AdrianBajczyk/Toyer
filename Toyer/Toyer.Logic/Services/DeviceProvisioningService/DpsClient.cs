using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Toyer.Logic.Services.DeviceProvisioningService;

public class DpsClient : IDpsClient
{
    // zapytaj jak ukryć te klucze oraz co z historią w repozytorium
    const string PRIMARY_KEY = @"C90S9vkT8XptudZY7bk/W1p0WQP0gZ5oYILnyQQEFdY7vm1o0Ozt9uoBckkDTW4gBJzs8k5m970yamZQ9vTNlQ==";
    const string ID_SCOPE = "0ne00BAD41A";
    const string GLOBAL_ENDPOINT = "global.azure-devices-provisioning.net";
    private readonly ILogger _logger;

    public DpsClient(ILogger logger)
    {
        _logger = logger;
    }

    public async Task RegisterDevice(Guid id)
    {
        _logger.LogInformation("Initializing the device provisioning client...");

        string DESIRED_DEVICE_ID = id.ToString();

        var derivedKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(PRIMARY_KEY), DESIRED_DEVICE_ID);

        using var security = new SecurityProviderSymmetricKey(
            DESIRED_DEVICE_ID,
            derivedKey,
            null);

        using var transportHandler = GetTransportHandler();

        ProvisioningDeviceClient provClient = ProvisioningDeviceClient.Create(
            GLOBAL_ENDPOINT,
            ID_SCOPE,
            security,
            transportHandler);

        _logger.LogInformation($"Initialized for device Id {security.GetRegistrationID()}.");

        _logger.LogInformation("Registering with the device provisioning service...");
        try
        {
            DeviceRegistrationResult result = await provClient.RegisterAsync();

            if (result.Status != ProvisioningRegistrationStatusType.Assigned)
            {
                _logger.LogError($"Registration failed. Error: " + result.ErrorMessage);
                return;
            }

            _logger.LogInformation($"Device {result.DeviceId} registered to {result.AssignedHub}.");

            _logger.LogInformation("Creating symmetric key authentication for IoT Hub...");

            IAuthenticationMethod auth = new DeviceAuthenticationWithRegistrySymmetricKey(
                result.DeviceId,
                security.GetPrimaryKey());

            _logger.LogInformation($"Testing the provisioned device with IoT Hub...");
            using DeviceClient iotClient = DeviceClient.Create(result.AssignedHub, auth, TransportType.Mqtt);

            _logger.LogInformation("Sending a telemetry message...");
            using var message = new Message(Encoding.UTF8.GetBytes("TestMessage"));
            await iotClient.SendEventAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        _logger.LogInformation("Finished...");
    }

    private ProvisioningTransportHandler GetTransportHandler()
    {
        return new ProvisioningTransportHandlerHttp();
    }

    private string ComputeDerivedSymmetricKey(byte[] masterKey, string registrationId)
    {
        using (var hmac = new HMACSHA256(masterKey))
        {
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationId)));
        }
    }
}
