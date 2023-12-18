namespace HeroesDecode.Models;

public class DecodePlayer
{
    public string Name { get; set; } = string.Empty;

    public string BattleTagName { get; set; } = string.Empty;

    public string? PlayerToonId { get; set; }

    public string? PlayerShortcutId { get; set; }

    public PlayerType PlayerType { get; set; }

    public PlayerHero? PlayerHero { get; set; } = null;

    public PlayerLoadout PlayerLoadout { get; set; } = new PlayerLoadout();

    public List<HeroMasteryTier> HeroMasteryTier { get; set; } = new();

    public StormTeam Team { get; set; }

    public int Handicap { get; set; }

    public bool IsWinner { get; set; } = false;

    public bool IsSilenced { get; set; } = false;

    public bool? IsVoiceSilenced { get; set; }

    public bool? IsBlizzardStaff { get; set; }

    public bool IsAutoSelect { get; set; } = false;

    public bool? HasActiveBoost { get; set; }

    public int? AccountLevel { get; set; }

    public long? PartyValue { get; set; }

    public ComputerDifficulty ComputerDifficulty { get; set; }

    public List<MatchAwardType>? MatchAwards { get; set; } = new();

    public List<HeroTalent> HeroTalents { get; set; } = new();

    public ScoreResult? ScoreResult { get; set; }
}
