namespace HeroesDecode.Models;

public class DecodePlayerDisconnect
{
    public string LeaveReason { get; set; } = string.Empty;

    public TimeSpan DisconnectTime { get; set; }

    public TimeSpan? RejoinTime { get; set; }
}
