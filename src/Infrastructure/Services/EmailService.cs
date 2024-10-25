using Application.Contracts.Infrastructure.Models.Emails;
using Application.Contracts.Infrastructure.Services;
using Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services;

public sealed class EmailService(IOptions<MailOption> mailOption) : IEmailService
{
    private readonly MailOption _mailOption = mailOption.Value;

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        MimeMessage email = new()
        {
            From =
            {
                new MailboxAddress(_mailOption.SenderDisplayName, _mailOption.SenderEmail)
            },
            To =
            {
                MailboxAddress.Parse(mailRequest.EmailTo)
            },
            Subject = mailRequest.Subject,
            Body = new TextPart(TextFormat.Text)
            {
                Text = mailRequest.Body
            }
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_mailOption.SmtpServer, _mailOption.SmtpPort, SecureSocketOptions.StartTls);

        await smtpClient.AuthenticateAsync(_mailOption.SenderEmail, _mailOption.SmtpPassword);

        await smtpClient.SendAsync(email);

        await smtpClient.DisconnectAsync(true);
    }
}
