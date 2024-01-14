using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;
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

    public async Task RegisterDevice(string desiredDeviceId, string pkSubsection)
    {
        _logger.LogInformation("Initializing the device provisioning client...");

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


            DeviceRegistrationResult result = await provClient.RegisterAsync();

            if (result.Status != ProvisioningRegistrationStatusType.Assigned)
            {
                throw new IotHubException(result.ErrorMessage);
            }

            IAuthenticationMethod auth = new DeviceAuthenticationWithRegistrySymmetricKey(
                result.DeviceId,
                security.GetPrimaryKey());

            using DeviceClient iotClient = DeviceClient.Create(result.AssignedHub, auth, TransportType.Mqtt);

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
