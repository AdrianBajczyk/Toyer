using Toyer.Logic.Services.DeviceMessaging;
using Toyer.Logic.Services.DeviceProvisioningService;

namespace Toyer.API.Extensions.WebAppBuilder
{
    internal static class AzureServicesExtensions
    {
        public static IServiceCollection AddCustomAzureServices(this IServiceCollection services)
        {
            services.AddSingleton<IDeviceMessageService, DeviceMessageService>();
            services.AddSingleton<IDpsClient, DpsClient>();

            return services;
        }
    }
}