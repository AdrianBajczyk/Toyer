﻿using Toyer.API.Controllers;
using Toyer.Logic.Services.Repositories.Classes;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Extensions.WebAppBuilder;

internal static class RepositoryServicesExtensions
{
    internal static IServiceCollection AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddScoped<IDeviceTypeRepository, SqlDeviceTypeRepository>();
        services.AddScoped<IOrderRepository, SqlOrderRepository>();
        services.AddScoped<IDeviceRepository, SqlDeviceRepository>();
        services.AddScoped<IDeviceAssignRepository, SqlDeviceAssignRepository>();

        return services;
    }
}