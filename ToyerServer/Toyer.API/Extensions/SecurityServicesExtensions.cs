﻿using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Toyer.Logic.Services.Authorization.AuthorizationHandlers;
using Toyer.Logic.Services.Authorization.Token;

namespace Toyer.API.Extensions;

public static class SecurityServicesExtensions
{
    public static IServiceCollection AddCustomSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var client = services.BuildServiceProvider().GetRequiredService<SecretClient>();

        //JWT
        services.AddScoped<ITokenService, TokenService>();

        var issuer = configuration["TokenValidationParameters:Issuer"];
        var audience = configuration["TokenValidationParameters:Audience"];
        var issuerSigningKey = client.GetSecret("IssuerSigningKey").Value.Value.ToString();

        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(issuerSigningKey))
        {
            throw new InvalidOperationException("Invalid configuration for JWT authentication.");
        }

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey))
                };


            });


        //POLICIES
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalHost",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials(); // Allow cookies
                });

            options.AddPolicy("Production",
        builder =>
        {
            builder.WithOrigins("https://toyer.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // Allow cookies
        });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Production", policy => policy.RequireRole("Employee", "Administrator"));
        });

        services.AddTransient<IAuthorizationHandler, PermissionHandler>();

        return services;
    }
}
