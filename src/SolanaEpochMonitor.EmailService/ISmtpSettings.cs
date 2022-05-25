using System;
namespace SolanaEpochMonitor.EmailService
{
    public interface ISmtpSettings
    {
        string Host { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
