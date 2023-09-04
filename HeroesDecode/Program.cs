// options
using Heroes.StormReplayParser.Pregame.Player;
using System.Data;

Option<string> replayPathOption = new(
    "--replay-path",
    description: "File path of a Heroes of the Storm .StormReplay file or a directory")
{
    IsRequired = true,
};

replayPathOption.AddAlias("-p");

Option<bool> resultOnlyOption = new(
    "--result-only",
    getDefaultValue: () => false,
    description: "Will only show result of parsing, no map info or player info; --show-player-talents and --show-player-stats options will be overridden to false");

Option<bool> showPlayerTalentsOption = new(
    "--show-player-talents",
    getDefaultValue: () => false,
    description: "Shows the player's talent information");

showPlayerTalentsOption.AddAlias("-t");

Option<bool> showPlayerStatsOption = new(
    "--show-player-stats",
    getDefaultValue: () => false,
    description: "Shows the player's stats");

showPlayerStatsOption.AddAlias("-s");

// pregame command
Option<string> battlelobbyPathOption = new(
    "--battlelobby-path",
    description: "File path of a Heroes of the Storm .battlelobby file or a directory")
{
    IsRequired = true,
};

battlelobbyPathOption.AddAlias("-p");

Command pregameCommand = new("pregame", "View Heroes of the Storm battlelobby file data information.")
{
    battlelobbyPathOption,
};

pregameCommand.SetHandler(
    (battlelobbyPath) =>
    {
        if (File.Exists(battlelobbyPath))
        {
            PregameParse(battlelobbyPath);
        }
        else if (Directory.Exists(battlelobbyPath))
        {
            string filePath = Path.Join(battlelobbyPath, "replay.server.battlelobby");
            if (File.Exists(filePath))
            {
                PregameParse(filePath);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No file found.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No file or directory found.");
            Console.ResetColor();
        }
    },
    battlelobbyPathOption);

// rootcommand
RootCommand rootCommand = new("View Heroes of the Storm replay file data information")
{
    replayPathOption,
    resultOnlyOption,
    showPlayerTalentsOption,
    showPlayerStatsOption,
    pregameCommand,
};

rootCommand.SetHandler(
    (replayPath, resultOnly, showPlayerTalents, showPlayerStats) =>
    {
        _resultOnly = resultOnly;
        if (!resultOnly)
        {
            _showPlayerTalents = showPlayerTalents;
            _showPlayerStats = showPlayerStats;
        }

        string? fileName = Path.GetFileName(replayPath);
        string? topDirectory = Path.GetDirectoryName(replayPath);
        if (!string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(topDirectory) && fileName == "[last]")
        {
            string? lastFile = new DirectoryInfo(topDirectory).GetFiles("*.StormReplay").OrderByDescending(x => x.LastWriteTimeUtc).FirstOrDefault()?.FullName;
            if (string.IsNullOrWhiteSpace(lastFile))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No file found.");
                Console.ResetColor();
            }
            else
            {
                Parse(lastFile, resultOnly);
            }
        }
        else if (File.Exists(replayPath))
        {
            Parse(replayPath, resultOnly);
        }
        else if (Directory.Exists(replayPath))
        {
            foreach (string? replayFile in Directory.EnumerateFiles(replayPath, "*.StormReplay", SearchOption.AllDirectories))
            {
                if (!string.IsNullOrEmpty(replayFile) && File.Exists(replayFile))
                    Parse(replayFile, resultOnly);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No file or directory found.");
            Console.ResetColor();
        }
    },
    replayPathOption,
    resultOnlyOption,
    showPlayerTalentsOption,
    showPlayerStatsOption);

await rootCommand.InvokeAsync(args);

static void Parse(string replayPath, bool onlyResult)
{
    StormReplayResult stormReplayResult = StormReplay.Parse(replayPath, new ParseOptions()
    {
        AllowPTR = true,
        ShouldParseTrackerEvents = true,
        ShouldParseGameEvents = true,
        ShouldParseMessageEvents = true,
    });

    ResultLine(stormReplayResult);

    if (!onlyResult)
    {
        GetInfo(stormReplayResult);
    }
}

static void ResultLine(StormReplayResult stormReplayResult)
{
    if (stormReplayResult.Status == StormReplayParseStatus.Success)
        Console.ForegroundColor = ConsoleColor.Green;
    else
        Console.ForegroundColor = ConsoleColor.Red;

    if (!_resultOnly)
    {
        Console.WriteLine(stormReplayResult.Status);
        Console.ResetColor();

        if (stormReplayResult.Status != StormReplayParseStatus.Success && stormReplayResult.Exception is not null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stormReplayResult.Exception.StackTrace);
            Console.ResetColor();
            _failed = true;
        }
    }
    else
    {
        Console.Write(stormReplayResult.Status);
        Console.ResetColor();
        Console.WriteLine($" [{Path.GetFileName(stormReplayResult.FileName)}] [{stormReplayResult.Replay.ReplayVersion}]");
    }
}

static void GetInfo(StormReplayResult stormReplayResult)
{
    StormReplay replay = stormReplayResult.Replay;

    List<StormPlayer> players = replay.StormPlayers.ToList();

    Console.WriteLine($"{"File Name: ",_infoFieldWidth}{Path.GetFileName(stormReplayResult.FileName)}");
    Console.WriteLine($"{"Game Mode: ",_infoFieldWidth}{replay.GameMode}");
    Console.WriteLine($"{"Map: ",_infoFieldWidth}{replay.MapInfo.MapName} [{replay.MapInfo.MapId}]");
    Console.WriteLine($"{"Version: ",_infoFieldWidth}{replay.ReplayVersion}");
    Console.WriteLine($"{"Region: ",_infoFieldWidth}{replay.Region}");
    Console.WriteLine($"{"Game Time: ",_infoFieldWidth}{replay.ReplayLength}");
    Console.WriteLine($"{"Lobby: ",_infoFieldWidth}{replay.LobbyMode}");
    Console.WriteLine($"{"Ready Mode: ",_infoFieldWidth}{replay.ReadyMode}");
    Console.WriteLine($"{"First Drft: ",_infoFieldWidth}{replay.FirstDraftTeam}");
    Console.WriteLine($"{"Ban Mode: ",_infoFieldWidth}{replay.BanMode}");
    Console.WriteLine($"{"Privacy: ",_infoFieldWidth}{replay.GamePrivacy}");

    if (replay.BanMode != StormBanMode.NotUsingBans)
    {
        TeamBansDisplay(replay.GetTeamBans(StormTeam.Blue), StormTeam.Blue);
        TeamBansDisplay(replay.GetTeamBans(StormTeam.Red), StormTeam.Red);
    }

    if (_failed)
        return;

    IEnumerable<StormPlayer> blueTeam = players.Where(x => x.Team == StormTeam.Blue);
    IEnumerable<StormPlayer> redTeam = players.Where(x => x.Team == StormTeam.Red);

    List<StormPlayer> observerPlayers = replay.StormObservers.ToList();

    StormTeamDisplay(replay, blueTeam, StormTeam.Blue);
    StormTeamDisplay(replay, redTeam, StormTeam.Red);
    StormTeamDisplay(replay, observerPlayers, StormTeam.Observer);

    Console.WriteLine();
}

static void StormTeamDisplay(StormReplay replay, IEnumerable<StormPlayer> players, StormTeam team)
{
    Dictionary<long, PartyIconColor> partyPlayers = new();
    bool partyPurpleUsed = false;
    bool partyRedUsed = false;

    string teamName = string.Empty;

    if (team == StormTeam.Blue)
        teamName = "Team Blue";
    else if (team == StormTeam.Red)
        teamName = "Team Red";
    else if (team == StormTeam.Observer)
        teamName = "Observers";

    if ((replay.WinningTeam == StormTeam.Blue && team == StormTeam.Blue) ||
        (replay.WinningTeam == StormTeam.Red && team == StormTeam.Red))
        teamName += " (Winner)";

    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Console.WriteLine(teamName);
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    if (players.Any())
    {
        foreach (StormPlayer player in players)
        {
            PartyIconColor? partyIcon = null;

            if (player.PartyValue.HasValue)
            {
                if (partyPlayers.TryGetValue(player.PartyValue.Value, out PartyIconColor partyIconColor))
                {
                    partyIcon = partyIconColor;
                }
                else
                {
                    if (player.Team == StormTeam.Blue)
                    {
                        if (partyPurpleUsed)
                            partyIcon = PartyIconColor.Blue;
                        else
                            partyIcon = PartyIconColor.Purple;

                        partyPurpleUsed = true;
                    }
                    else if (player.Team == StormTeam.Red)
                    {
                        if (partyRedUsed)
                            partyIcon = PartyIconColor.Orange;
                        else
                            partyIcon = PartyIconColor.Red;

                        partyRedUsed = true;
                    }

                    partyPlayers.Add(player.PartyValue.Value, partyIcon!.Value);
                }
            }

            PlayerInfo(player, partyIcon);
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("(NONE)");
    }
}

static void PlayerInfo(StormPlayer player, PartyIconColor? partyIcon)
{
    if (player is null)
        throw new ArgumentNullException(nameof(player));

    if (player.PlayerType != PlayerType.Computer)
    {
        // party
        if (partyIcon.HasValue)
            Console.Write($"[{(int)partyIcon}]");
        else
            Console.Write($"{"[-]"}");

        // battletag
        if (!string.IsNullOrEmpty(player.BattleTagName))
            Console.WriteLine($"\u001b[1m{"Player: ",_playerFieldWidth - 3}{player.BattleTagName}\u001b[0m");
        else
            Console.WriteLine($"\u001b[1m{"Player: ",_playerFieldWidth - 3}{player.Name}\u001b[0m");

        // account level
        if (player.AccountLevel.HasValue && player.AccountLevel.Value > 0)
            Console.WriteLine($"{"Player Level: ",_playerFieldWidth}{player.AccountLevel.Value}");
        else
            Console.WriteLine($"{"Player Level: ",_playerFieldWidth}[Level:???]");

        // toon handle
        Console.WriteLine($"{"Player Toon: ",_playerFieldWidth}{player.ToonHandle}");
    }
    else if (player.PlayerType == PlayerType.Computer)
    {
        Console.WriteLine($"[-] {player.Name}");
    }

    if (player.PlayerHero is not null)
    {
        if (player.HeroMasteryTiers.ToDictionary(x => x.HeroAttributeId, x => x.TierLevel).TryGetValue(player.PlayerHero.HeroAttributeId, out int tierLevel))
        {
            if (tierLevel == 2 && player.PlayerHero.HeroLevel < 25)
                player.PlayerHero.HeroLevel = 25;
            else if (tierLevel == 3 && player.PlayerHero.HeroLevel < 50)
                player.PlayerHero.HeroLevel = 50;
            else if (tierLevel == 4 && player.PlayerHero.HeroLevel < 75)
                player.PlayerHero.HeroLevel = 75;
            else if (tierLevel == 5 && player.PlayerHero.HeroLevel < 100)
                player.PlayerHero.HeroLevel = 100;
        }
    }

    if (player.PlayerType != PlayerType.Observer)
    {
        // hero name
        Console.WriteLine($"{"Hero Name: ",_playerFieldWidth}{player.PlayerHero!.HeroName} [{player.PlayerHero.HeroUnitId}]");

        // hero level
        if (player.IsAutoSelect)
            Console.WriteLine($"{"Hero Level: ",_playerFieldWidth}Auto");
        else
            Console.WriteLine($"{"Hero Level: ",_playerFieldWidth}{player.PlayerHero.HeroLevel}");

        if (player.MatchAwards != null)
        {
            foreach (MatchAwardType matchAwardType in player.MatchAwards)
            {
                Console.WriteLine($"{"Award: ",_playerFieldWidth}{matchAwardType}");
            }
        }

        // talents
        if (_showPlayerTalents)
        {
            Console.WriteLine();
            Console.WriteLine(" \u001b[4mTalents\u001b[0m");

            Console.Write($"{"Level 1:",10}");
            if (player.Talents.Count >= 1)
                Console.WriteLine($" {player.Talents[0].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 4:",10}");
            if (player.Talents.Count >= 2)
                Console.WriteLine($" {player.Talents[1].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 7:",10}");
            if (player.Talents.Count >= 3)
                Console.WriteLine($" {player.Talents[2].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 10:",10}");
            if (player.Talents.Count >= 4)
                Console.WriteLine($" {player.Talents[3].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 13:",10}");
            if (player.Talents.Count >= 5)
                Console.WriteLine($" {player.Talents[4].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 16:",10}");
            if (player.Talents.Count >= 6)
                Console.WriteLine($" {player.Talents[5].TalentNameId}");
            else
                Console.WriteLine();

            Console.Write($"{"Level 20:",10}");
            if (player.Talents.Count >= 7)
                Console.WriteLine($" {player.Talents[6].TalentNameId}");
            else
                Console.WriteLine();

            Console.WriteLine();
        }

        // stats
        if (_showPlayerStats)
        {
            Console.WriteLine();
            Console.WriteLine("\u001b[4mStatistics\u001b[0m");

            if (player.ScoreResult != null)
            {
                Console.WriteLine("\u001b[4mCombat\u001b[0m");
                Console.WriteLine($"{"Hero Kills:",_statisticsFieldWidth} {player.ScoreResult.SoloKills}");
                Console.WriteLine($"{"Assists:",_statisticsFieldWidth} {player.ScoreResult.Assists}");
                Console.WriteLine($"{"Takedowns:",_statisticsFieldWidth} {player.ScoreResult.Takedowns}");
                Console.WriteLine($"{"Deaths:",_statisticsFieldWidth} {player.ScoreResult.Deaths}");

                Console.WriteLine("\u001b[4mSiege\u001b[0m");
                Console.WriteLine($"{"Minion Damage:",_statisticsFieldWidth} {player.ScoreResult.MinionDamage}");
                Console.WriteLine($"{"Summon Damage:",_statisticsFieldWidth} {player.ScoreResult.SummonDamage}");
                Console.WriteLine($"{"Structure Damage:",_statisticsFieldWidth} {player.ScoreResult.StructureDamage}");
                Console.WriteLine($"{"Total Siege Damage:",_statisticsFieldWidth} {player.ScoreResult.SiegeDamage}");

                Console.WriteLine("\u001b[4mHero\u001b[0m");
                Console.WriteLine($"{"Hero Damage:",_statisticsFieldWidth} {player.ScoreResult.HeroDamage}");

                if (player.ScoreResult.DamageTaken > 0)
                    Console.WriteLine($"{"Damage Taken:",_statisticsFieldWidth} {player.ScoreResult.DamageTaken}");
                else
                    Console.WriteLine($"{"Damage Taken:",_statisticsFieldWidth}");

                if (player.ScoreResult.Healing > 0)
                    Console.WriteLine($"{"Healing/Shielding:",_statisticsFieldWidth} {player.ScoreResult.Healing}");
                else
                    Console.WriteLine($"{"Healing/Shielding:",_statisticsFieldWidth}");

                if (player.ScoreResult.SelfHealing > 0)
                    Console.WriteLine($"{"Self Healing:",_statisticsFieldWidth} {player.ScoreResult.SelfHealing}");
                else
                    Console.WriteLine($"{"Self Healing:",_statisticsFieldWidth}");

                Console.WriteLine($"{"Experience:",_statisticsFieldWidth} {player.ScoreResult.ExperienceContribution}");

                Console.WriteLine("\u001b[4mTime\u001b[0m");
                Console.WriteLine($"{"Spent Dead:",_statisticsFieldWidth} {player.ScoreResult.TimeSpentDead}");
                Console.WriteLine($"{"Rooting Heroes:",_statisticsFieldWidth} {player.ScoreResult.TimeRootingEnemyHeroes}");
                Console.WriteLine($"{"Silence Heroes:",_statisticsFieldWidth} {player.ScoreResult.TimeSilencingEnemyHeroes}");
                Console.WriteLine($"{"Stun Heroes:",_statisticsFieldWidth} {player.ScoreResult.TimeStunningEnemyHeroes}");
                Console.WriteLine($"{"CC Heroes:",_statisticsFieldWidth} {player.ScoreResult.TimeCCdEnemyHeroes}");
                Console.WriteLine($"{"On Fire:",_statisticsFieldWidth} {player.ScoreResult.OnFireTimeonFire}");

                Console.WriteLine("\u001b[4mOther\u001b[0m");
                if (player.ScoreResult.SpellDamage.HasValue && player.ScoreResult.SpellDamage.Value > 0)
                    Console.WriteLine($"{"Spell Damage:",_statisticsFieldWidth} {player.ScoreResult.SpellDamage}");
                else
                    Console.WriteLine($"{"Spell Damage:",_statisticsFieldWidth}");

                if (player.ScoreResult.PhysicalDamage.HasValue && player.ScoreResult.PhysicalDamage.Value > 0)
                    Console.WriteLine($"{"Physical Damage:",_statisticsFieldWidth} {player.ScoreResult.PhysicalDamage}");
                else
                    Console.WriteLine($"{"Physical Damage:",_statisticsFieldWidth}");

                Console.WriteLine($"{"Merc Damage:",_statisticsFieldWidth} {player.ScoreResult.CreepDamage}");
                Console.WriteLine($"{"Merc Camp Captures:",_statisticsFieldWidth} {player.ScoreResult.MercCampCaptures}");
                Console.WriteLine($"{"Watch Tower Captures:",_statisticsFieldWidth} {player.ScoreResult.WatchTowerCaptures}");
                Console.WriteLine($"{"Town Kills:",_statisticsFieldWidth} {player.ScoreResult.TownKills}");
                Console.WriteLine($"{"Town Kills:",_statisticsFieldWidth} {player.ScoreResult.TownKills}");
                Console.WriteLine($"{"Minion Kills:",_statisticsFieldWidth} {player.ScoreResult.MinionKills}");
                Console.WriteLine($"{"Regen Globes:",_statisticsFieldWidth} {player.ScoreResult.RegenGlobes}");
            }
            else
            {
                Console.WriteLine("(NOT AVAILABLE)");
            }

            Console.WriteLine();
        }
    }
}

static void PregameParse(string battlelobbyPath)
{
    StormReplayPregameResult stormReplayPregameResult = StormReplayPregame.Parse(battlelobbyPath);

    PregameResultLine(stormReplayPregameResult);

    if (_failed)
        return;

    PregameGetInfo(stormReplayPregameResult);
    Console.WriteLine();
}

static void PregameResultLine(StormReplayPregameResult stormReplayPregameResult)
{
    if (stormReplayPregameResult.Status == StormReplayPregameParseStatus.Success)
        Console.ForegroundColor = ConsoleColor.Green;
    else
        Console.ForegroundColor = ConsoleColor.Red;

    Console.WriteLine(stormReplayPregameResult.Status);
    Console.ResetColor();

    if (stormReplayPregameResult.Status != StormReplayPregameParseStatus.Success && stormReplayPregameResult.Exception is not null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(stormReplayPregameResult.Exception.StackTrace);
        Console.ResetColor();
        _failed = true;
    }
}

static void PregameGetInfo(StormReplayPregameResult stormReplayPregameResult)
{
    StormReplayPregame replay = stormReplayPregameResult.ReplayBattleLobby;

    List<StormPregamePlayer> players = replay.StormPlayers.ToList();

    Console.WriteLine($"{"Game Mode: ",_infoFieldWidth}{replay.GameMode}");
    Console.WriteLine($"{"Map Link: ",_infoFieldWidth}{replay.MapLink}");
    Console.WriteLine($"{"Build: ",_infoFieldWidth}{replay.ReplayBuild}");
    Console.WriteLine($"{"Region: ",_infoFieldWidth}{replay.Region}");
    Console.WriteLine($"{"Lobby: ",_infoFieldWidth}{replay.LobbyMode}");
    Console.WriteLine($"{"Ready Mode: ",_infoFieldWidth}{replay.ReadyMode}");
    Console.WriteLine($"{"First Drft: ",_infoFieldWidth}{replay.FirstDraftTeam}");
    Console.WriteLine($"{"Ban Mode: ",_infoFieldWidth}{replay.BanMode}");
    Console.WriteLine($"{"Privacy: ",_infoFieldWidth}{replay.GamePrivacy}");

    if (replay.BanMode != StormBanMode.NotUsingBans)
    {
        TeamBansDisplay(replay.GetTeamBans(StormTeam.Blue), StormTeam.Blue);
        TeamBansDisplay(replay.GetTeamBans(StormTeam.Red), StormTeam.Red);
    }

    List<StormPregamePlayer> observerPlayers = replay.StormObservers.ToList();

    if (StormGameMode.NormalGameModes.HasFlag(replay.GameMode) || replay.GameMode == StormGameMode.Cooperative)
    {
        // assume first 5 players are team blue
        IEnumerable<StormPregamePlayer> blueTeam = replay.StormPlayers.Take(5);
        IEnumerable<StormPregamePlayer> redTeam = replay.StormPlayers.Skip(5).Take(5);

        PregameStormTeamDisplay(replay, blueTeam, StormTeam.Blue);
        PregameStormTeamDisplay(replay, redTeam, StormTeam.Red);
    }
    else if (replay.GameMode == StormGameMode.Brawl)
    {
        IEnumerable<StormPregamePlayer> blueTeam = replay.StormPlayers.Take(5);
        PregameStormTeamDisplay(replay, blueTeam, StormTeam.Blue);

        if (replay.PlayersCount > 5)
        {
            IEnumerable<StormPregamePlayer> redTeam = replay.StormPlayers.Skip(5).Take(5);
            PregameStormTeamDisplay(replay, redTeam, StormTeam.Red);
        }
    }
    else
    {
        // for custom games, players are mixed, no way to tell which player on team
        PregameStormTeamDisplay(replay, replay.StormPlayers, StormTeam.Unknown);
    }

    PregameStormTeamDisplay(replay, observerPlayers, StormTeam.Observer);
}

static void PregameStormTeamDisplay(StormReplayPregame replay, IEnumerable<StormPregamePlayer> players, StormTeam team)
{
    Dictionary<long, PartyIconColor> partyPlayers = new();
    bool partyPurpleUsed = false;
    bool partyRedUsed = false;
    bool partyBlueUsed = false;
    bool partyOrangeUsed = false;

    string teamName = string.Empty;

    if (team == StormTeam.Blue)
        teamName = "Team Blue";
    else if (team == StormTeam.Red)
        teamName = "Team Red";
    else if (team == StormTeam.Observer)
        teamName = "Observers";
    else if (team == StormTeam.Unknown)
        teamName = "Unknown";

    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Console.WriteLine(teamName);
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    if (players.Any())
    {
        foreach (StormPregamePlayer player in players)
        {
            PartyIconColor? partyIcon = null;

            if (player.PartyValue.HasValue)
            {
                if (partyPlayers.TryGetValue(player.PartyValue.Value, out PartyIconColor partyIconColor))
                {
                    partyIcon = partyIconColor;
                }
                else
                {
                    if (team == StormTeam.Blue)
                    {
                        if (partyPurpleUsed)
                            partyIcon = PartyIconColor.Blue;
                        else
                            partyIcon = PartyIconColor.Purple;

                        partyPurpleUsed = true;
                    }
                    else if (team == StormTeam.Red)
                    {
                        if (partyRedUsed)
                            partyIcon = PartyIconColor.Orange;
                        else
                            partyIcon = PartyIconColor.Red;

                        partyRedUsed = true;
                    }
                    else if (team == StormTeam.Unknown)
                    {
                        if (!partyBlueUsed)
                        {
                            partyIcon = PartyIconColor.Blue;
                            partyBlueUsed = true;
                        }
                        else if (!partyPurpleUsed)
                        {
                            partyIcon = PartyIconColor.Purple;
                            partyPurpleUsed = true;
                        }
                        else if (!partyRedUsed)
                        {
                            partyIcon = PartyIconColor.Red;
                            partyRedUsed = true;
                        }
                        else if (!partyOrangeUsed)
                        {
                            partyIcon = PartyIconColor.Orange;
                            partyOrangeUsed = true;
                        }
                    }

                    partyPlayers.Add(player.PartyValue.Value, partyIcon!.Value);
                }
            }

            PregamePlayerInfo(player, partyIcon);
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("(NONE)");
    }
}

static void PregamePlayerInfo(StormPregamePlayer player, PartyIconColor? partyIcon)
{
    if (player is null)
        throw new ArgumentNullException(nameof(player));

    if (player.PlayerType != PlayerType.Computer)
    {
        // party
        if (partyIcon.HasValue)
            Console.Write($"[{(int)partyIcon}]");
        else
            Console.Write($"{"[-]"}");

        // battletag
        if (!string.IsNullOrEmpty(player.BattleTagName))
            Console.WriteLine($"\u001b[1m{"Player: ",_playerFieldWidth - 3}{player.BattleTagName}\u001b[0m");
        else
            Console.WriteLine($"\u001b[1m{"Player: ",_playerFieldWidth - 3}{player.Name}\u001b[0m");

        // account level
        if (player.AccountLevel.HasValue && player.AccountLevel.Value > 0)
            Console.WriteLine($"{"Player Level: ",_playerFieldWidth}{player.AccountLevel.Value}");
        else
            Console.WriteLine($"{"Player Level: ",_playerFieldWidth}[Level:???]");

        // toon handle
        Console.WriteLine($"{"Player Toon: ",_playerFieldWidth}{player.ToonHandle}");
    }
    else if (player.PlayerType == PlayerType.Computer)
    {
        Console.WriteLine($"[-] {player.Name}");
    }

    int? hmt = player.PlayerHero!.HeroMasteryTier;

    if (hmt.HasValue)
    {
        if (hmt.Value == 2 && player.PlayerHero.HeroLevel < 25)
            player.PlayerHero.HeroLevel = 25;
        else if (hmt.Value == 3 && player.PlayerHero.HeroLevel < 50)
            player.PlayerHero.HeroLevel = 50;
        else if (hmt.Value == 4 && player.PlayerHero.HeroLevel < 75)
            player.PlayerHero.HeroLevel = 75;
        else if (hmt.Value == 5 && player.PlayerHero.HeroLevel < 100)
            player.PlayerHero.HeroLevel = 100;
    }

    if (player.PlayerType != PlayerType.Observer)
    {
        // hero level
        if (string.IsNullOrWhiteSpace(player.PlayerHero.HeroAttributeId))
        {
            Console.WriteLine($"{"Hero Level: ",_playerFieldWidth}Auto");
            Console.WriteLine($"{"Hero AttId: ",_playerFieldWidth}Auto");
        }
        else
        {
            Console.WriteLine($"{"Hero Level: ",_playerFieldWidth}{player.PlayerHero.HeroLevel}");
            Console.WriteLine($"{"Hero AttId: ",_playerFieldWidth}{player.PlayerHero!.HeroAttributeId}");
        }
    }
}

static void TeamBansDisplay(IReadOnlyList<string?> teamBans, StormTeam team)
{
    Console.WriteLine();
    Console.WriteLine($"Team {team} Bans:");

    for (int i = 0; i < teamBans.Count; i++)
    {
        DisplayBans(teamBans, i);
    }

    static void DisplayBans(IReadOnlyList<string?> teamBans, int i)
    {
        string? item = teamBans[i];

        string heroAttId = string.Empty;
        if (string.IsNullOrWhiteSpace(item))
            heroAttId = "<NONE>";
        else
            heroAttId = item;

        Console.WriteLine($"{$"Ban {i + 1}: ",_infoFieldWidth}{heroAttId}");
    }
}

public partial class Program
{
    private const int _infoFieldWidth = 12;
    private const int _playerFieldWidth = 14;
    private const int _statisticsFieldWidth = 21;

    private static bool _resultOnly = false;
    private static bool _showPlayerTalents = false;
    private static bool _showPlayerStats = false;

    private static bool _failed = false;
}