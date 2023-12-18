namespace HeroesDecode.Models;

public class DecodeGameEvents
{
    public string? Player { get; set; }

    public TimeSpan TimeStamp { get; set; }

    public StormGameEventType? GameEventType { get; set; }

    public string? Data { get; set; } = string.Empty;
}
