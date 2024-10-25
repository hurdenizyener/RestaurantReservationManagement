using Application.Contracts.Infrastructure.Models.Emails;

namespace Application.Contracts.Infrastructure.Services;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
