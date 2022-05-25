namespace SolanaEpochMonitor.EmailService
{
    public interface IEmailSettings
    {
        string FromAddress { get; set; }
        string FromName { get; set; }
        string ToAddress { get; set; }
        string Subject { get; set; }
    }
}
