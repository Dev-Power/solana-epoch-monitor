using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace SolanaEpochMonitor.WorkerService;

public class PersistanceService
{
    private const string DB_PATH = "/db.json";
    
    public int GetLastNotifiedEpoch()
    {
        if (!File.Exists(DB_PATH)) return 0;
            
        var rawContents = File.ReadAllText(DB_PATH);
        return JsonObject.Parse(rawContents)["lastNotifiedEpoch"].GetValue<int>();
    }

    public void SaveLastNotifiedEpoch(int newEpoch)
    {
        var newValue = new {lastNotifiedEpoch = newEpoch};
        File.WriteAllText(DB_PATH, JsonConvert.SerializeObject(newValue));
    }
}