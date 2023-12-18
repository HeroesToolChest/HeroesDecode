namespace HeroesDecode.Models;

public class DecodeTrackerEvent
{
    public StormTrackerEventType TrackerEventType { get; set; }

    public TimeSpan Timestamp { get; set; }

    public string? Data { get; set; }
}
