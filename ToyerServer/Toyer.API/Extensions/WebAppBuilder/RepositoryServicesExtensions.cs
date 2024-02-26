using Toyer.API.Controllers;
using Toyer.Logic.Services.Repositories.Classes;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class RepositoryServicesExtensions
{
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddScoped<IDeviceTypeRepository, SqlDeviceTypeRepository>();
        services.AddScoped<IOrderRepository, SqlOrderRepository>();
        services.AddScoped<IDeviceRepository, SqlDeviceRepository>();
        services.AddScoped<IDeviceAssignmentRepository, SqlDeviceAssignmentRepository>();
        services.AddScoped<ITokenRepository, SqlTokenRepository>();
        services.AddScoped<IMessageServiceRepository, MessageServiceRepository>();

        return services;
    }
}
