namespace HeroesDecode.Models;

public sealed class DecodeMapInfo
{
    public string MapName { get; set; } = string.Empty;

    public string? MapId { get; set; }

    public Point MapSize { get; set; }
}
