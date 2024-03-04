# Heroes Decode
[![Build](https://github.com/HeroesToolChest/HeroesDecode/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/HeroesToolChest/HeroesDecode/actions/workflows/build.yml)
[![Release](https://img.shields.io/github/release/HeroesToolChest/HeroesDecode.svg)](https://github.com/HeroesToolChest/HeroesDecode/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/HeroesDecode.svg)](https://www.nuget.org/packages/HeroesDecode/)

Heroes Decode is a .NET command line tool that displays data from Heroes of the Storm replay files (.StormReplay) as well as displaying data from a match loading screen by parsing the `.battlelobby` file.

Displayed data can either be in a limited formatted display or in json format.

The parsing is done by the library [Heroes Storm Replay Parser](https://github.com/HeroesToolChest/Heroes.StormReplayParser).

## Installation
### Dotnet Global Tool (Recommended)
Download and install the [.NET 8.0 SDK](https://dotnet.microsoft.com/download). 

Once installed, run the following command:
```
dotnet tool install --global HeroesDecode
```

Installing via this method also allows easy updating to future versions using the following command:
```
dotnet tool update --global HeroesDecode
```

***

### Zip File Download - Framework-Dependent Deployment (fdd)
Portable to any operating system.

Download and install the [.NET 8.0 Runtime or SDK](https://dotnet.microsoft.com/download). 

Download and extract the latest `HeroesDecode.*-fdd-any` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page.

***

### Zip File Download - Framework-Dependent Executable (fde)
Runs only on the selected operating system.

Download and install the [.NET 8.0 Runtime or SDK](https://dotnet.microsoft.com/download). 

Download and extract the latest `HeroesDecode.*-fde-<OS>-x64` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page for a selected operating system.

***

### Zip File Download - Self-Contained Deployment (scd)
Runs only on the selected operating system.

No runtime or SDK is required.

Download and extract the latest `HeroesDecode.*-scd-<OS>-x64` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page for a selected operating system.

This zip file contains everything that is needed to run the dotnet app without .NET being installed, so the zip file is quite large.

## Usage
If installed as a Dotnet Global Tool, the app can be run with one of the following commands:
```
dotnet heroes-decode -h
dotnet-heroes-decode -h
```

If installed as a Framework-Dependent Deployment (fdd), run the following command from the extracted directory:
```
dotnet heroesdecode.dll -h
```

If installed as a Framework-Dependent Executable (fde) or Self-Contained Deployment (scd), run one of the following commands from the extracted directory:
```
windows (powershell): .\heroesdecode -h 
macOS or Linux: ./heroesdecode -h
```

Output of the -h option
```
Description:
  View Heroes of the Storm replay file data

Usage:
  HeroesDecode [command] [options]

Options:
  -p, --replay-path <replay-path> (REQUIRED)  File path of a Heroes of the Storm .StormReplay file or a directory
  --result-only                               Will only show result of parsing, no map info or player info; --show-player-talents and --show-player-stats options will be overridden to false [default: False]
  -t, --show-player-talents                   Shows the player's talent information [default: False]
  -s, --show-player-stats                     Shows the player's stats [default: False]
  --version                                   Show version information
  -?, -h, --help                              Show help and usage information

Commands:
  pregame           View Heroes of the Storm battlelobby file data.
  get-json          Get the data from the replay as json
  get-pregame-json  Get the data from the replay battlelobby as json
```

Example command to parse a replay file.
```
dotnet heroesdecode.dll --replay-path 'C:\ReplayFiles\2020-08-07 15.33.52 Lost Cavern.StormReplay'
```
Example output from the previous command.
```
Success
 File Name: BattleFieldOfEternity.StormReplay
 Game Mode: Custom
       Map: Battlefield of Eternity [BattlefieldOfEternity]
   Version: 2.51.1.80702
    Region: US
 Game Time: 00:17:25
     Lobby: TournamentDraft
Ready Mode: FCFS
First Drft: CoinToss
  Ban Mode: MidBan
   Privacy: NoMatchHistory

Team Blue Bans:
     Ban 1: Genj
     Ban 2: Diab
     Ban 3: <NONE>

Team Red Bans:
     Ban 1: Maie
     Ban 2: Tra0
     Ban 3: <NONE>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Blue (Winner)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-]   Player: Apple#1111
Player Level: 1000
 Player Toon: 1-Hero-1-1122334
   Hero Name: Jaina [HeroJaina]
  Hero Level: Auto
       Award: MostVengeancesPerformed
	   
[0]   Player: Banana#2222
Player Level: 500
 Player Toon: 1-Hero-1-2121212
   Hero Name: Li-Ming [HeroWizard]
  Hero Level: 12
       Award: MVP
	   
[0]   Player: Grapes#3333
Player Level: 1566
 Player Toon: 1-Hero-1-4343434
   Hero Name: Zul'jin [HeroZuljin]
  Hero Level: 100
       Award: MostHeroDamageDone
	   
[0]   Player: Mango#4444
Player Level: 1961
 Player Toon: 1-Hero-1-7878788
   Hero Name: Tychus [HeroTychus]
  Hero Level: Auto
  
[-]   Player: Orange#5555
Player Level: 633
 Player Toon: 1-Hero-1-5454545
   Hero Name: Stukov [HeroStukov]
  Hero Level: Auto
       Award: MostHealing
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Red
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-]   Player: Grapefruit#6666
Player Level: 1885
 Player Toon: 1-Hero-1-22222
   Hero Name: Kharazim [HeroMonk]
  Hero Level: Auto
  
[5]   Player: Avacado#7777
Player Level: 960
 Player Toon: 1-Hero-1-4343434
   Hero Name: Raynor [HeroRaynor]
  Hero Level: 5
  
[5]   Player: Strawberries#8888
Player Level: 637
 Player Toon: 1-Hero-1-111111
   Hero Name: Mephisto [HeroMephisto]
  Hero Level: 15
  
[-]   Player: Lemon#9999
Player Level: 1466
 Player Toon: 1-Hero-1-22222
   Hero Name: Qhira [HeroNexusHunter]
  Hero Level: Auto
  
[-]   Player: Olive#0000
Player Level: 2030
 Player Toon: 1-Hero-1-666666
   Hero Name: Johanna [HeroCrusader]
  Hero Level: Auto
       Award: MostTeamfightDamageTaken
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Observers
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
(NONE)
```

## Options
### Replay Path (-p, --replay-path)

Can be either the path to a replay file or a directory. If set as a directory recommended to set the option `--result-only`.

The file path can also be set to `[last]` to parse the latest created replay file in the directory.

Example of using `[last]`.
```
--replay-path 'C:\ReplayFiles\[last]'
```

***

### Result Only (--result-only)

Only the result of the parsing as well as the file name and the replay version will be outputted. This will also override the options `--show-players-talents` and `--show-player-stats` to false.

Example.
```
Success [2019-06-20 19.23.36 Garden of Terror.StormReplay] [2.46.0.74739]
```

***

### Show Player Talents (-t, --show-player-talents)

Shows the player's talents.

Example.
```
Talents
  Level 1: AnaShrikeVampiricRounds
  Level 4: AnaOverdose
  Level 7: AnaSleepDartNightTerrors
 Level 10: AnaHeroicAbilityNanaBoost
 Level 13: AnaHealingDartSmellingSalts
 Level 16: AnaDetachableBoxMagazine
 Level 20: AnaDynamicOptics
 ```
 
***

### Show Player Stats(-s, --show-player-stats)

Shows the player's statistics.

Example.
```
Statistics
Combat
          Hero Kills: 9
             Assists: 31
           Takedowns: 40
              Deaths: 2
Siege
       Minion Damage: 49665
       Summon Damage: 0
    Structure Damage: 9843
  Total Siege Damage: 59508
Hero
         Hero Damage: 91884
        Damage Taken: 119834
   Healing/Shielding:
        Self Healing: 33557
          Experience: 27230
Time
          Spent Dead: 00:00:54
      Rooting Heroes: 00:00:00
      Silence Heroes: 00:00:00
         Stun Heroes: 00:01:54
           CC Heroes: 00:11:58
             On Fire: 00:00:40
Other
        Spell Damage:
     Physical Damage:
         Merc Damage: 0
  Merc Camp Captures: 0
Watch Tower Captures: 0
          Town Kills: 0
          Town Kills: 0
        Minion Kills: 0
        Regen Globes: 0
```

## Commands
### pregame
```
Description:
  View Heroes of the Storm battlelobby file data.

Usage:
  HeroesDecode pregame [options]

Options:
  -p, --battlelobby-path <battlelobby-path> (REQUIRED)  File path of a Heroes of the Storm .battlelobby file or a directory
  -?, -h, --help                                        Show help and usage information
```

Parses a `replay.server.battlelobby` file. The `.battlelobby` file is created at the start of the loading screen.

On Windows, the default location is `C:\<USER PATH>\AppData\Local\Temp\Heroes of the Storm\TempWriteReplayP1\replay.server.battlelobby`. The `Temp\Heroes of the Storm` directory is deleted after the game (not the match) has closed.

Example output:
```
Success
 Game Mode: ARAM
       Map: Industrial District [IndustrialDistrict]
     Build: 91418
    Region: US
     Lobby: Standard
Ready Mode: FCFS
First Drft: CoinToss
  Ban Mode: NotUsingBans
   Privacy: Normal
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Blue
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-]   Player: Apple#1111
Player Level: 1463
 Player Toon: 1-Hero-1-1122334
  Hero Level: 10
  Hero AttId: Murk

[0]   Player: Banana#2222
Player Level: 477
 Player Toon: 1-Hero-1-2121212
  Hero Level: 100
  Hero AttId: Tass

[0]   Player: Grapes#3333
Player Level: 393
 Player Toon: 1-Hero-1-4343434
  Hero Level: 200
  Hero AttId: Nova

[0]   Player: Mango#4444
Player Level: 896
 Player Toon: 1-Hero-1-7878788
  Hero Level: 3000
  Hero AttId: VALE

[-]   Player: Orange#5555
Player Level: 882
 Player Toon: 1-Hero-1-5454545
  Hero Level: 54
  Hero AttId: Vari

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Red
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-]   Player: Grapefruit#6666
Player Level: 3000
 Player Toon: 1-Hero-1-22222
  Hero Level: 1
  Hero AttId: Tra0

[-]   Player: Avacado#7777
Player Level: 1000
 Player Toon: 1-Hero-1-4343434
  Hero Level: 1
  Hero AttId: Genj

[-]   Player: Strawberries#8888
Player Level: 2000
 Player Toon: 1-Hero-1-111111
  Hero Level: 1
  Hero AttId: Abat

[5]   Player: Lemon#9999
Player Level: 800
 Player Toon: 1-Hero-1-22222
  Hero Level: 1
  Hero AttId: Mdvh

[5]   Player: Olive#0000
Player Level: 1900
 Player Toon: 1-Hero-1-666666
  Hero Level: 1
  Hero AttId: DECK

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Observers
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
(NONE)
```
### get-json
```
Description:
  Get the data from the replay as json

Usage:
  HeroesDecode get-json [options]

Options:
  -p, --replay-path <replay-path> (REQUIRED)  File path of a Heroes of the Storm .StormReplay file or a directory
  --parse-message-events                      Allow the parsing of the message events [default: True]
  --parse-tracker-events                      Allow the parsing of tracker events [default: True]
  --parse-game-events                         Allow the parsing of the game events [default: True]
  --has-tracker-events                        Adds the tracker events to the output json [default: False]
  --has-game-events                           Adds the game events to the output json [default: False]
  --include-all-message-events                Includes all the message type events (default is only chat type messages) [default: False]
  --no-json-display                           Doesn't display the json to the terminal [default: False]
  -o, --output-directory <output-directory>   Set the directory for the output json file
  -?, -h, --help                              Show help and usage information
```
Returns the parsed data in json format. By default, it will be returned to the terminal. To output to a file, set the `--output-directory` option.

View example json output file [here](https://github.com/HeroesToolChest/HeroesDecode/blob/main/JsonOutput/replay.json). 

Json file was generated with the following options:
- parse-message-events (true)
- parse-tracker-events (true)
- parse-game-events (true)
- has-tracker-events (false)
- has-game-events (false)
- all message events taken out

### get-pregame-json
```
Description:
  Get the data from the replay battlelobby as json

Usage:
  HeroesDecode get-pregame-json [options]

Options:
  -p, --battlelobby-path <battlelobby-path> (REQUIRED)  File path of a Heroes of the Storm .battlelobby file or a directory
  --no-json-display                                     Doesn't display the json to the terminal [default: False]
  -o, --output-directory <output-directory>             Set the directory for the output json file
  -?, -h, --help                                        Show help and usage information
```
Returns the parsed data in json format. By default, it will be returned to the terminal. To output to a file, set the `--output-directory` option.

View example json output file [here](https://github.com/HeroesToolChest/HeroesDecode/blob/main/JsonOutput/replay-pregame-battlelobby.json).

## Developing
To build and compile the code, it is recommended to use the latest version of [Visual Studio 2022 or Visual Studio Code](https://visualstudio.microsoft.com/downloads/).

Another option is to use the dotnet CLI tools from the [.NET 8.0 SDK](https://dotnet.microsoft.com/download).

The main project is `HeroesDecode.csproj` and the main entry point is `Program.cs`.

## License
[MIT license](/LICENSE)
