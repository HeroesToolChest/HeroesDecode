namespace HeroesDecode;

public sealed class JsonAdditonalOptions
{
    public bool HasTrackEvents { get; set; }

    public bool HasGameEvents { get; set; }

    public bool IncludeAllMessageEvents { get; set; }

    public bool NoJsonDislay { get; set; }

    public string? OutputDirectory { get; set; }
}
