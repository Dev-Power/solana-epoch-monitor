using System.Text;

namespace SolanaEpochMonitor.WorkerService;

public class AppSettings
{
    public string ApiUrl { get; set; } 
    public int CheckIntervalInMinutes { get; set; } = 60;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"ApiUrl: {ApiUrl}");
        sb.AppendLine($"CheckIntervalInMinutes: {CheckIntervalInMinutes}");
        return sb.ToString();
    }
}