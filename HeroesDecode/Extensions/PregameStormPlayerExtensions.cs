namespace HeroesDecode.Extensions;

public static class PregameStormPlayerExtensions
{
    extension(PregameStormPlayer pregameStormPlayer)
    {
        public DecodePlayerPregame ToDecodePlayerPregame() => new()
        {
            AccountLevel = pregameStormPlayer.AccountLevel,
            BattleTagName = pregameStormPlayer.BattleTagName,
            HasActiveBoost = pregameStormPlayer.HasActiveBoost,
            IsBlizzardStaff = pregameStormPlayer.IsBlizzardStaff,
            IsSilenced = pregameStormPlayer.IsSilenced,
            IsVoiceSilenced = pregameStormPlayer.IsVoiceSilenced,
            Name = pregameStormPlayer.Name,
            PartyValue = pregameStormPlayer.PartyValue,
            ComputerDifficulty = pregameStormPlayer.ComputerDifficulty,
            PlayerHero = pregameStormPlayer.PlayerHero,
            PlayerLoadout = pregameStormPlayer.PlayerLoadout,
            PlayerShortcutId = pregameStormPlayer.ToonHandle?.ShortcutId,
            PlayerToonId = pregameStormPlayer.ToonHandle?.ToString(),
            PlayerSlotType = pregameStormPlayer.PlayerSlotType,
            PlayerType = pregameStormPlayer.PlayerType,
        };
    }
}
