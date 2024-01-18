using Toyer.Logic.Services.EmailService;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class EmailServiceExtensions
{
    public static IServiceCollection AddCustomEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();

        services.AddSingleton(emailConfig);
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}