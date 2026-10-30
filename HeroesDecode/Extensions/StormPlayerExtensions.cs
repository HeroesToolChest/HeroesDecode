namespace HeroesDecode.Extensions;

public static class StormPlayerExtensions
{
    extension(StormPlayer stormPlayer)
    {
        public DecodePlayer ToDecodePlayer()
        {
            return new()
            {
                Name = stormPlayer.Name,
                PlayerToonId = stormPlayer.ToonHandle?.ToString(),
                PlayerShortcutId = stormPlayer.ToonHandle?.ShortcutId,
                PlayerType = stormPlayer.PlayerType,
                PlayerHero = stormPlayer.PlayerHero,
                PlayerLoadout = stormPlayer.PlayerLoadout,
                HeroMasteryTier = [.. stormPlayer.HeroMasteryTiers],
                Team = stormPlayer.Team,
                Handicap = stormPlayer.Handicap,
                IsWinner = stormPlayer.IsWinner,
                IsSilenced = stormPlayer.IsSilenced,
                IsVoiceSilenced = stormPlayer.IsVoiceSilenced,
                IsBlizzardStaff = stormPlayer.IsBlizzardStaff,
                IsAutoSelect = stormPlayer.IsAutoSelect,
                HasActiveBoost = stormPlayer.HasActiveBoost,
                IsPlatformMac = stormPlayer.IsPlatformMac,
                IsBanChooser = stormPlayer.IsBanChooser,
                IsReplayOwner = stormPlayer.IsReplayOwner,
                BattleTagName = stormPlayer.BattleTagName,
                AccountLevel = stormPlayer.AccountLevel,
                PartyValue = stormPlayer.PartyValue,
                PortraitLicenseId = stormPlayer.PortraitLicenseId,
                PortraitUnlockableId = stormPlayer.PortraitUnlockableId,
                PortraitBorderUnlockableId = stormPlayer.PortraitBorderUnlockableId,
                ComputerDifficulty = stormPlayer.ComputerDifficulty,
                MatchAwards = stormPlayer.MatchAwards?.ToList(),
                HeroTalents = [.. stormPlayer.Talents],
                Disconnects = [.. stormPlayer.PlayerDisconnects.Select(x => x.ToDecodePlayerDisconnect())],
                LicenseIds = [.. stormPlayer.LicenseIds],
                ScoreResult = stormPlayer.ScoreResult,
            };
        }
    }
}
