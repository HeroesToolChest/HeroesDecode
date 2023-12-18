namespace HeroesDecode.Models;

internal class DecodePlayerPregame
{
    public string Name { get; set; } = string.Empty;

    public string BattleTagName { get; set; } = string.Empty;

    public string? PlayerToonId { get; set; }

    public string? PlayerShortcutId { get; set; }

    public PlayerSlotType PlayerSlotType { get; set; }

    public PlayerType PlayerType { get; set; }

    public PregamePlayerHero? PlayerHero { get; set; }

    public PregamePlayerLoadout PlayerLoadout { get; set; } = new();

    public bool IsSilenced { get; set; }

    public bool? IsVoiceSilenced { get; set; }

    public bool? IsBlizzardStaff { get; set; }

    public bool? HasActiveBoost { get; set; }

    public int? AccountLevel { get; set; }

    public long? PartyValue { get; set; }

    public ComputerDifficulty ComputerDifficulty { get; set; }
}
