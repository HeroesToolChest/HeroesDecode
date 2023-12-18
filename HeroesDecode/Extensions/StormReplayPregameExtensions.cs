namespace HeroesDecode.Extensions;

internal static class StormReplayPregameExtensions
{
    public static DecodeReplayPregame ToDecodeReplayPregame(this StormReplayPregame stormReplayPregame)
    {
        return new()
        {
            BanMode = stormReplayPregame.BanMode,
            DisabledHeroes = stormReplayPregame.DisabledHeroes.ToList(),
            FirstDraftTeam = stormReplayPregame.FirstDraftTeam,
            GameMode = stormReplayPregame.GameMode,
            GamePrivacy = stormReplayPregame.GamePrivacy,
            GameSpeed = stormReplayPregame.GameSpeed,
            IsBattleLobbyParsed = stormReplayPregame.IsBattleLobbyPlayerInfoParsed,
            LobbyMode = stormReplayPregame.LobbyMode,
            MapInfo =
            {
                MapId = stormReplayPregame.MapId,
                MapLink = stormReplayPregame.MapLink,
            },
            ObserversCount = stormReplayPregame.PlayersObserversCount,
            PlayersAndAICount = stormReplayPregame.PlayersCount,
            PlayersAndObserversCount = stormReplayPregame.PlayersWithObserversCount,
            RandomValue = stormReplayPregame.RandomValue,
            ReadyMode = stormReplayPregame.ReadyMode,
            Region = stormReplayPregame.Region,
            ReplayBuild = stormReplayPregame.ReplayBuild,
            TeamSize = stormReplayPregame.TeamSize,
            TeamBans =
            {
                {
                    StormTeam.Blue, stormReplayPregame.GetTeamBans(StormTeam.Blue).ToList()
                },
                {
                    StormTeam.Red, stormReplayPregame.GetTeamBans(StormTeam.Red).ToList()
                },
            },
            Players = stormReplayPregame.StormPlayers.Select(x => x.ToDecodePlayerPregame()).ToList(),
            Observers = stormReplayPregame.StormObservers.Select(x => x.ToDecodePlayerPregame()).ToList(),
        };
    }
}
