using Kora.Interfaces;
using Kora.Models;
using Kora.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Kora.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    
    public MailService(IOptions<MailSettings> options)
    {
        _mailSettings = options.Value;
    }
    
    public async Task<bool> DispatchMailAsync(MailDataModel model)
    {
        try
        {
            //MimeMessage - a class from Mimekit
            MimeMessage email_Message = new MimeMessage();
            MailboxAddress email_From = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
            email_Message.From.Add(email_From);
            MailboxAddress email_To = new MailboxAddress(model.EmailToName, model.EmailToId);
            email_Message.To.Add(email_To);
            email_Message.Subject = model.EmailSubject;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = model.EmailBody;
            email_Message.Body = emailBodyBuilder.ToMessageBody();
            //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
            SmtpClient MailClient = new SmtpClient();
            MailClient.Connect(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSSL);
            MailClient.Authenticate(_mailSettings.EmailId, _mailSettings.Password);
            MailClient.Send(email_Message);
            MailClient.Disconnect(true);
            MailClient.Dispose();
            
            return await Task.FromResult(true);
        }
        catch(Exception ex)
        {
            // Exception Details
            return await Task.FromResult(false);
        }
    }
}