using System.Text.Json.Nodes;
using SolanaEpochMonitor.EmailService;

namespace SolanaEpochMonitor.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly AppSettings _settings;
    private readonly IEmailService _emailService;

    public Worker(ILogger<Worker> logger, AppSettings settings, IEmailService emailService)
    {
        _logger = logger;
        _settings = settings;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string apiUrl = _settings.ApiUrl;
        int interval = _settings.CheckIntervalInMinutes;
        
        _logger.LogInformation(FormatLogMessage($"Worker started with the following:\n{_settings}"));
        
        var apiClient = new SolanaApiClient(apiUrl);
        var persistenceService = new PersistanceService();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var getEpochInfoResponse = await apiClient.GetEpochInfo();

            var lastNotifiedEpoch = persistenceService.GetLastNotifiedEpoch();
            if (getEpochInfoResponse.Result.Epoch != lastNotifiedEpoch)
            {
                var message = FormatLogMessage($"New epoch detected: {getEpochInfoResponse.Result.Epoch}");
                _logger.LogInformation(message);
                
                await _emailService.SendNotification(message);
                persistenceService.SaveLastNotifiedEpoch(getEpochInfoResponse.Result.Epoch);
                return;
            }

            await Task.Delay(interval * 60 * 1000, stoppingToken);
        }
    }

    private string FormatLogMessage(string message)
    {
        return $"[{DateTime.UtcNow.ToString()}] {message}";
    }
}