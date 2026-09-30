namespace HeroesDecode.Models;

public sealed class DecodeTeamChatMessage
{
    public string? PlayerSender { get; set; }

    public TimeSpan Timestamp { get; set; }

    public StormMessageTarget MessageTarget { get; set; }

    public string Text { get; set; } = string.Empty;
}
