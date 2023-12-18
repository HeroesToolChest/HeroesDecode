namespace HeroesDecode.Models;

public class DecodeReplay
{
    public string Version { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public int ElapsedGamesLoops { get; set; }

    public TimeSpan ReplayLength { get; set; }

    public long RandomValue { get; set; }

    public string? ReplayOwner { get; set; } = string.Empty;

    public bool HasObservers { get; set; }

    public bool HasAI { get; set; }

    public int PlayersAndAICount { get; set; }

    public int PlayersAndObserversCount { get; set; }

    public int ObserversCount { get; set; }

    public string TeamSize { get; set; } = string.Empty;

    public DecodeMapInfo MapInfo { get; } = new();

    public StormGameMode GameMode { get; set; }

    public StormGameSpeed GameSpeed { get; set; }

    public StormGamePrivacy GamePrivacy { get; set; }

    public StormReadyMode ReadyMode { get; set; }

    public StormLobbyMode LobbyMode { get; set; }

    public StormBanMode BanMode { get; set; }

    public StormFirstDraftTeam FirstDraftTeam { get; set; }

    public StormRegion Region { get; set; }

    public StormTeam WinningTeam { get; set; }

    public List<DecodePlayer> Players { get; set; } = new();

    public List<DecodePlayer> Observers { get; set; } = new();

    public List<DecodeDraftPick> DraftPicks { get; set; } = new();

    public Dictionary<StormTeam, List<string?>> TeamBans { get; set; } = new();

    public Dictionary<StormTeam, int?> TeamFinalLevel { get; set; } = new();

    public Dictionary<StormTeam, List<StormTeamLevel>?> TeamLevels { get; set; } = new();

    public Dictionary<StormTeam, List<StormTeamXPBreakdown>?> TeamXPBreakdown { get; set; } = new();

    public List<DecodeMessage> Messages { get; set; } = new();

    public List<DecodeTrackerEvent> TrackerEvents { get; set; } = new();

    public List<DecodeGameEvents> GameEvents { get; set; } = new();

    public List<string> DisabledHeroes { get; set; } = new();

    public bool IsBattleLobbyParsed { get; set; }
}
