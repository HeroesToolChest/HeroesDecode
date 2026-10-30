namespace HeroesDecode.Models;

public sealed class DecodePlayer
{
    public string Name { get; set; } = string.Empty;

    public string BattleTagName { get; set; } = string.Empty;

    public string? PlayerToonId { get; set; }

    public string? PlayerShortcutId { get; set; }

    public PlayerType PlayerType { get; set; }

    public PlayerHero? PlayerHero { get; set; } = null;

    public PlayerLoadout PlayerLoadout { get; set; } = new();

    public List<HeroMasteryTier> HeroMasteryTier { get; set; } = [];

    public StormTeam Team { get; set; }

    public int Handicap { get; set; }

    public bool IsWinner { get; set; } = false;

    public bool IsSilenced { get; set; } = false;

    public bool? IsVoiceSilenced { get; set; }

    public bool? IsBlizzardStaff { get; set; }

    public bool IsAutoSelect { get; set; } = false;

    public bool? HasActiveBoost { get; set; }

    public bool IsPlatformMac { get; set; }

    public bool IsBanChooser { get; set; }

    public bool IsReplayOwner { get; set; }

    public int? AccountLevel { get; set; }

    public long? PartyValue { get; set; }

    public long? PortraitLicenseId { get; set; }

    public long? PortraitUnlockableId { get; set; }

    public long? PortraitBorderUnlockableId { get; set; }

    public ComputerDifficulty ComputerDifficulty { get; set; }

    public List<MatchAwardType>? MatchAwards { get; set; } = [];

    public List<HeroTalent> HeroTalents { get; set; } = [];

    public List<DecodePlayerDisconnect> Disconnects { get; set; } = [];

    public IList<long> LicenseIds { get; set; } = [];

    public ScoreResult? ScoreResult { get; set; }
}
