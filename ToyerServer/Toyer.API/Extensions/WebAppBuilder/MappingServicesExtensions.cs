using Toyer.Logic.Services.Mappings;
using Toyer.Logic.Services.Mappings.UserMappings;
using Toyer.Logic.Services.Mappings.UserMappings.Classes;
using Toyer.Logic.Services.Mappings.UserMappings.Interfaes;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class MappingServicesExtensions
{
    public static IServiceCollection AddCustomMappingServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        services.AddScoped<IUserMapings, UserMappings>();
        services.AddScoped<IDeviceTypeMappings, DeviceTypeMappings>();
        services.AddScoped<IOrderMappings, OrderMappings>();
        services.AddScoped<IDeviceMappings, DeviceMappings>();
        services.AddScoped<IUserDevicesMappings, UserDevicesMappings>();

        return services;
    }
}