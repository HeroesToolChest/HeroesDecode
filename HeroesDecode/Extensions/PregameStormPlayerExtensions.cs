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
            IsBanChooser = pregameStormPlayer.IsBanChooser,
            IsBlizzardStaff = pregameStormPlayer.IsBlizzardStaff,
            IsReplayOwner = pregameStormPlayer.IsReplayOwner,
            IsSilenced = pregameStormPlayer.IsSilenced,
            IsVoiceSilenced = pregameStormPlayer.IsVoiceSilenced,
            Name = pregameStormPlayer.Name,
            PartyValue = pregameStormPlayer.PartyValue,
            PortraitUnlockableId = pregameStormPlayer.PortraitUnlockableId,
            PortraitBorderUnlockableId = pregameStormPlayer.PortraitBorderUnlockableId,
            ComputerDifficulty = pregameStormPlayer.ComputerDifficulty,
            HeroMasteryTier = [.. pregameStormPlayer.HeroMasteryTiers],
            LicenseIds = [.. pregameStormPlayer.LicenseIds],
            PlayerHero = pregameStormPlayer.PlayerHero,
            PlayerLoadout = pregameStormPlayer.PlayerLoadout,
            PlayerShortcutId = pregameStormPlayer.ToonHandle?.ShortcutId,
            PlayerToonId = pregameStormPlayer.ToonHandle?.ToString(),
            PlayerSlotType = pregameStormPlayer.PlayerSlotType,
            PlayerType = pregameStormPlayer.PlayerType,
            Team = pregameStormPlayer.Team,
        };
    }
}
