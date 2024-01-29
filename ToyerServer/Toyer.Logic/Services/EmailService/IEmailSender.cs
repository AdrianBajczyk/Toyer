namespace Toyer.Logic.Services.EmailService;

public interface IEmailSender
{
    void SendEmail(EmailMessage message);
    Task SendEmailAsync(EmailMessage message);
}