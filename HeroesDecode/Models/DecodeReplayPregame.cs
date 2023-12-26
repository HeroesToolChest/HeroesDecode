namespace HeroesDecode.Models;

internal class DecodeReplayPregame
{
    public int ReplayBuild { get; set; }

    public long RandomValue { get; set; }

    public int PlayersAndAICount { get; set; }

    public int PlayersAndObserversCount { get; set; }

    public int ObserversCount { get; set; }

    public string TeamSize { get; set; } = string.Empty;

    public DecodeMapInfoPregame MapInfo { get; } = new();

    public StormGameMode GameMode { get; set; }

    public StormGameSpeed GameSpeed { get; set; }

    public StormGamePrivacy GamePrivacy { get; set; }

    public StormReadyMode ReadyMode { get; set; }

    public StormLobbyMode LobbyMode { get; set; }

    public StormBanMode BanMode { get; set; }

    public StormFirstDraftTeam FirstDraftTeam { get; set; }

    public StormRegion Region { get; set; }

    public List<DecodePlayerPregame> Players { get; set; } = [];

    public List<DecodePlayerPregame> Observers { get; set; } = [];

    public Dictionary<StormTeam, List<string?>> TeamBans { get; set; } = [];

    public List<string> DisabledHeroes { get; set; } = [];

    public bool IsBattleLobbyParsed { get; set; }
}
