using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Toyer.Logic.Services.DeviceProvisioningService;

public class DpsClient : IDpsClient
{

    private readonly IConfiguration _configuration;
    private readonly ILogger<DpsClient> _logger;

    public DpsClient(ILogger<DpsClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task RegisterDevice(Guid id, string pkSubsection)
    {
        _logger.LogInformation("Initializing the device provisioning client...");

        string desiredDeviceId = id.ToString();
        string dpsEnrollmentPrimaryKey = _configuration[$"DpsConfig:PrimaryKey:{pkSubsection}"];
        string dpsGlobalEndpoint = _configuration["DpsConfig:GlobalEndpoint"];
        string dpsIdScope = _configuration["DpsConfig:IdScope"];

        var derivedKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(dpsEnrollmentPrimaryKey), desiredDeviceId);

        using var security = new SecurityProviderSymmetricKey(
            desiredDeviceId,
            derivedKey,
            null);

        using var transportHandler = GetTransportHandler();

        ProvisioningDeviceClient provClient = ProvisioningDeviceClient.Create(
            dpsGlobalEndpoint,
            dpsIdScope,
            security,
            transportHandler);

        _logger.LogInformation($"Initialized for device Id {security.GetRegistrationID()}.");
        _logger.LogInformation(security.GetPrimaryKey());

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

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
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
