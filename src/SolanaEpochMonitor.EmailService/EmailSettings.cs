namespace SolanaEpochMonitor.EmailService;

public class EmailSettings : IEmailSettings
{
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string ToAddress { get; set; }
    public string Subject { get; set; }
}
