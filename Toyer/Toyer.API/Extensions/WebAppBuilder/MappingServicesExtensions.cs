using Toyer.API.Controllers;
using Toyer.Data.Mappings;
using Toyer.Logic.Mappings.UserMappings.classes;
using Toyer.Logic.Mappings.UserMappings.Classes;

namespace Toyer.API.Extensions.WebAppBuilder
{
    internal static class MappingServicesExtensions
    {
        public static IServiceCollection AddCustomMappingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddScoped<IUserMapings, UserMappings>();
            services.AddScoped<IDeviceTypeMappings, DeviceTypeMappings>();
            services.AddScoped<IOrderMappings, OrderMappings>();
            services.AddScoped<IDeviceMappings, DeviceMappings>();

            return services;
        }
    }
}