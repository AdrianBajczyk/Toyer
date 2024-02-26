namespace Toyer.Logic.Services.Repositories.Interfaces
{
    public interface IMessageServiceRepository
    {
        Task SendEmailMessage(string senderEmail, string addresseeEmail, string message);
    }
}