using Newtonsoft.Json;
using RestSharp;

namespace SolanaEpochMonitor.WorkerService;

public class SolanaApiClient
{
    private readonly string _apiUrl;

    public SolanaApiClient(string apiUrl)
    {
        _apiUrl = apiUrl;
    }
    
    public async Task<GetEpochInfoResponse> GetEpochInfo()
    {
        var client = new RestClient(_apiUrl);
        var request = new RestRequest("/", Method.Post)
        {
            RequestFormat = DataFormat.Json,
        };
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(new { Id = Guid.NewGuid(), Method = "getEpochInfo", Jsonrpc = "2.0", Params = new string[] {} }); 
        
        var responseRaw = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<GetEpochInfoResponse>(responseRaw.Content);
    }
}