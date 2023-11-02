using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using OptiApp.Models;

namespace OptiApp.Service;

public class EmailSender:IEmailSender
{
    private readonly IConfiguration _configuration;
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration; 
    }
    public async Task SendEmailAsync(string to, string subject, string htmlMessage )
    {
        var req = new EmailRequestDto()
        {
            To = to,
            Subject = subject,
            Body = htmlMessage
        };
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("indlovutech@gmail.com"));
        email.To.Add(MailboxAddress.Parse(req.To));
        email.Subject = req.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html){
            Text = req.Body
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["SmtpSettings:SmtpHost"],int.Parse(_configuration["SmtpSettings:SmtpPort"]!),MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["SmtpSettings:SmtpName"],_configuration["SmtpSettings:SmtpPass"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}