namespace Application.Contracts.Infrastructure.Models.Emails;

public sealed class MailRequest(string emailTo, string subject, string body)
{
    public string EmailTo { get; } = emailTo;
    public string Subject { get; } = subject;
    public string Body { get; } = body;
}

