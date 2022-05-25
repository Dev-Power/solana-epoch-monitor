using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace SolanaEpochMonitor.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSettings _emailSettings;

        public EmailService(IEmailSettings emailSettings, ISmtpSettings smtpSettings)
        {
            if (string.IsNullOrEmpty(emailSettings.ToAddress)) throw new ArgumentException($"{nameof(emailSettings.ToAddress)} is mandatory");
            if (string.IsNullOrEmpty(emailSettings.FromAddress)) throw new ArgumentException($"{nameof(emailSettings.FromAddress)} is mandatory");
            if (string.IsNullOrEmpty(emailSettings.Subject)) throw new ArgumentException($"{nameof(emailSettings.Subject)} is mandatory");
            if (string.IsNullOrEmpty(emailSettings.FromName)) throw new ArgumentException($"{nameof(emailSettings.FromName)} is mandatory");

            if (string.IsNullOrEmpty(smtpSettings.Host)) throw new ArgumentException($"{nameof(smtpSettings.Host)} is mandatory");
            if (string.IsNullOrEmpty(smtpSettings.Username)) throw new ArgumentException($"{nameof(smtpSettings.Username)} is mandatory");
            if (string.IsNullOrEmpty(smtpSettings.Password)) throw new ArgumentException($"{nameof(smtpSettings.Password)} is mandatory");
            if (smtpSettings.Port == 0) throw new ArgumentException($"{nameof(smtpSettings.Port)} is mandatory");

            _emailSettings = emailSettings;
            
            var sender = new SmtpSender(() => new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password)
            });
            Email.DefaultSender = sender;
        }

        public async Task SendNotification(string notificationMessage)
        {
            var email = Email
                .From(_emailSettings.FromAddress, _emailSettings.FromName)
                .To(_emailSettings.ToAddress)
                .Subject(_emailSettings.Subject)
                .Body(notificationMessage);

            await email.SendAsync();
        }
    }
}
