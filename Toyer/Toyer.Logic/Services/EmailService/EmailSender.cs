﻿using MailKit.Net.Smtp;
using MimeKit;

namespace Toyer.Logic.Services.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;
    public EmailSender(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public void SendEmail(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }
    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
            client.Send(mailMessage);
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}