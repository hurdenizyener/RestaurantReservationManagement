namespace Infrastructure.Options;

public sealed class MailOption
{
    public const string Key = "MailOption";
    public string SenderDisplayName { get; set; } = default!;
    public string SenderEmail { get; set; } = default!;
    public string SmtpPassword { get; set; } = default!;
    public string SmtpServer { get; set; } = default!;
    public int SmtpPort { get; set; }

}