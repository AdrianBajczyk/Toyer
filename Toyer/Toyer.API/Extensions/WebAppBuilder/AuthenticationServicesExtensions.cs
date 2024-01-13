using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Toyer.Logic.Services.Authorization;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class AuthenticationServicesExtensions
{
    public static IServiceCollection AddCustomAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    ValidIssuer = configuration["TokenValidationParameters:Issuer"],
                    ValidAudience = configuration["TokenValidationParameters:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["IssuerSigningKey"]))
                };
            });


        return services;
    }
}
