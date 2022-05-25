namespace SolanaEpochMonitor.WorkerService;

public class Result
{
    public int AbsoluteSlot { get; set; }
    public int BlockHeight { get; set; }
    public int Epoch { get; set; }
    public int SlotIndex { get; set; }
    public int SlotsInEpoch { get; set; }
    public long TransactionCount { get; set; }
}

public class GetEpochInfoResponse
{
    public string Jsonrpc { get; set; }
    public Result Result { get; set; }
    public string Id { get; set; }
}