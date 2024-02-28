using Toyer.Logic.Services.EmailService;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class MessageServiceRepository(IEmailSender emailSender) : IMessageServiceRepository
{
    private readonly IEmailSender _emailSender = emailSender;

    public async Task SendEmailMessage(string senderEmail, string addresseeEmail, string messageContent)
    {
        var message = new EmailMessage(new string[] { addresseeEmail }, $"Email from client: {senderEmail}", messageContent);
        await _emailSender.SendEmailAsync(message);
    }
}
