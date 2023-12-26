namespace HeroesDecode.Extensions;

public static class StormPlayerExtensions
{
    public static DecodePlayer ToDecodePlayer(this StormPlayer stormPlayer)
    {
        return new()
        {
            Name = stormPlayer.Name,
            PlayerToonId = stormPlayer.ToonHandle?.ToString(),
            PlayerShortcutId = stormPlayer.ToonHandle?.ShortcutId,
            PlayerType = stormPlayer.PlayerType,
            PlayerHero = stormPlayer.PlayerHero,
            PlayerLoadout = stormPlayer.PlayerLoadout,
            HeroMasteryTier = stormPlayer.HeroMasteryTiers.ToList(),
            Team = stormPlayer.Team,
            Handicap = stormPlayer.Handicap,
            IsWinner = stormPlayer.IsWinner,
            IsSilenced = stormPlayer.IsSilenced,
            IsVoiceSilenced = stormPlayer.IsVoiceSilenced,
            IsBlizzardStaff = stormPlayer.IsBlizzardStaff,
            IsAutoSelect = stormPlayer.IsAutoSelect,
            HasActiveBoost = stormPlayer.HasActiveBoost,
            BattleTagName = stormPlayer.BattleTagName,
            AccountLevel = stormPlayer.AccountLevel,
            PartyValue = stormPlayer.PartyValue,
            ComputerDifficulty = stormPlayer.ComputerDifficulty,
            MatchAwards = stormPlayer.MatchAwards?.ToList(),
            HeroTalents = stormPlayer.Talents.ToList(),
            Disconnects = stormPlayer.PlayerDisconnects.Select(x => x.ToDecodePlayerDisconnect()).ToList(),
            ScoreResult = stormPlayer.ScoreResult,
        };
    }
}
