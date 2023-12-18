namespace HeroesDecode.Extensions;

public static class StormReplayExtensions
{
    public static DecodeReplay ToDecodeReplay(this StormReplay stormReplay)
    {
        return new()
        {
            HasObservers = stormReplay.HasObservers,
            HasAI = stormReplay.HasAI,
            Version = stormReplay.ReplayVersion.ToString(),
            ElapsedGamesLoops = stormReplay.ElapsedGamesLoops,
            ReplayLength = stormReplay.ReplayLength,
            Timestamp = stormReplay.Timestamp,
            RandomValue = stormReplay.RandomValue,
            TeamSize = stormReplay.TeamSize,
            PlayersAndAICount = stormReplay.PlayersCount,
            PlayersAndObserversCount = stormReplay.PlayersWithObserversCount,
            ObserversCount = stormReplay.PlayersObserversCount,
            GameMode = stormReplay.GameMode,
            GameSpeed = stormReplay.GameSpeed,
            GamePrivacy = stormReplay.GamePrivacy,
            ReadyMode = stormReplay.ReadyMode,
            LobbyMode = stormReplay.LobbyMode,
            BanMode = stormReplay.BanMode,
            FirstDraftTeam = stormReplay.FirstDraftTeam,
            Region = stormReplay.Region,
            WinningTeam = stormReplay.WinningTeam,
            ReplayOwner = stormReplay.Owner?.ToonHandle?.ToString(),
            MapInfo =
            {
                MapName = stormReplay.MapInfo.MapName,
                MapSize = stormReplay.MapInfo.MapSize,
                MapId = stormReplay.MapInfo.MapId,
            },
            DraftPicks = stormReplay.DraftPicks.Select(x => x.ToDecodeDraftPick()).ToList(),
            Messages = stormReplay.Messages.Select(x => x.ToDecodeMessage()).ToList(),
            Players = stormReplay.StormPlayers.Select(x => x.ToDecodePlayer()).ToList(),
            Observers = stormReplay.StormObservers.Select(x => x.ToDecodePlayer()).ToList(),
            TrackerEvents = stormReplay.TrackerEvents.Select(x => x.ToDecodeTrackerEvent()).ToList(),
            GameEvents = stormReplay.GameEvents.Select(x => x.ToDecodeGameEvents()).ToList(),
            IsBattleLobbyParsed = stormReplay.IsBattleLobbyPlayerInfoParsed,
            DisabledHeroes = stormReplay.DisabledHeroes.ToList(),
            TeamBans =
            {
                {
                    StormTeam.Blue, stormReplay.GetTeamBans(StormTeam.Blue).ToList()
                },
                {
                    StormTeam.Red, stormReplay.GetTeamBans(StormTeam.Red).ToList()
                },
            },
            TeamFinalLevel =
            {
                {
                    StormTeam.Blue, stormReplay.GetTeamFinalLevel(StormTeam.Blue)
                },
                {
                    StormTeam.Red, stormReplay.GetTeamFinalLevel(StormTeam.Red)
                },
            },
            TeamLevels =
            {
                {
                    StormTeam.Blue, stormReplay.GetTeamLevels(StormTeam.Blue)?.ToList()
                },
                {
                    StormTeam.Red, stormReplay.GetTeamLevels(StormTeam.Red)?.ToList()
                },
            },
            TeamXPBreakdown =
            {
                {
                    StormTeam.Blue, stormReplay.GetTeamXPBreakdown(StormTeam.Blue)?.ToList()
                },
                {
                    StormTeam.Red, stormReplay.GetTeamXPBreakdown(StormTeam.Red)?.ToList()
                },
            },
        };
    }
}
