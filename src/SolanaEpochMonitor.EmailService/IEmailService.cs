using System.Threading.Tasks;

namespace SolanaEpochMonitor.EmailService;

public interface IEmailService
{
    Task SendNotification(string notificationMessage);
}