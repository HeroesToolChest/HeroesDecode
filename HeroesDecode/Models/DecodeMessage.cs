namespace HeroesDecode.Models;

public class DecodeMessage
{
    public string? PlayerSender { get; set; }

    public TimeSpan Timestamp { get; set; }

    public StormMessageEventType MessageEventType { get; set; }

    public string Message { get; set; } = string.Empty;
}
