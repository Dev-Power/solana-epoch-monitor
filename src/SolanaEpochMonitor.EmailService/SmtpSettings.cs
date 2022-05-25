namespace SolanaEpochMonitor.EmailService;

public class SmtpSettings : ISmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}