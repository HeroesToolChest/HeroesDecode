# Heroes Decode
[![Build Status](https://dev.azure.com/kevinkoliva/Heroes%20of%20the%20Storm%20Projects/_apis/build/status/HeroesToolChest.HeroesDecode?branchName=master)](https://dev.azure.com/kevinkoliva/Heroes%20of%20the%20Storm%20Projects/_build/latest?definitionId=12&branchName=master)
[![Release](https://img.shields.io/github/release/HeroesToolChest/HeroesDecode.svg)](https://github.com/HeroesToolChest/HeroesDecode/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/HeroesDecode.svg)](https://www.nuget.org/packages/HeroesDecode/)

Heroes Decode is a .NET command line tool that parses Heroes of the Storm replay files (.StormReplay) and provides information about the parsed replay to the console.

The purpose of this tool is to provide a quick way to parse a replay and to obtain basic information at a glance. It is not meant to be used as a tool/library to obtain data from the replay (*i.e* this is not a parsing library).

The parsing is done by the library [Heroes Storm Replay Parser](https://github.com/HeroesToolChest/Heroes.StormReplayParser).

## Installation
### Dotnet Global Tool (Recommended)
Download and install the [.NET 5.0 SDK](https://dotnet.microsoft.com/download). 

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

Download and install the [.NET 5.0 Runtime or SDK](https://dotnet.microsoft.com/download). 

Download and extract the latest `HeroesDecode.*-fdd-any` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page.

***

### Zip File Download - Framework-Dependent Executable (fde)
Runs only on the selected operating system.

Download and install the [.NET 5.0 Runtime or SDK](https://dotnet.microsoft.com/download). 

Download and extract the latest `HeroesDecode.*-fde-<OS>-x64` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page for a selected operating system.

***

### Zip File Download - Self-Contained Deployment (scd)
Runs only on the selected operating system.

No runtime or SDK is required.

Download and extract the latest `HeroesDecode.*-scd-<OS>-x64` zip file from the [releases](https://github.com/HeroesToolChest/HeroesDecode/releases) page for a selected operating system.

This zip file contains everything that is needed to run the dotnet core app without .NET Core being installed, so the zip file is quite large.

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
windows (cmd): heroesdecode -h
windows (powershell): .\heroesdecode -h 
macOS or Linux: ./heroesdecode -h
```

Output of the -h option
```
heroesdecode:
  Parses Heroes of the Storm replay files

Usage:
  heroesdecode [options]

Options:
  -p, --replay-path <replay-path> (REQUIRED)    File path of a Heroes of the Storm .StormReplay file or a directory
  --result-only                                 Will only show result of parsing, no map info or player info; --show-player-talents and --show-player-stats options will be overridden to false
                                                [default: False]
  -t, --show-player-talents                     Shows the player's talent information [default: False]
  -s, --show-player-stats                       Shows the player's stats [default: False]
  --version                                     Show version information
  -?, -h, --help                                Show help and usage information
```

Example command to parse a replay file.
```
dotnet heroesdecode.dll --replay-path 'C:\ReplayFiles\2020-08-07 15.33.52 Lost Cavern.StormReplay'
```
Example output from the previous command.
```
Success
File Name: 2020-08-07 15.33.52 Lost Cavern.StormReplay
Game Mode: Brawl
      Map: Lost Cavern [ID:LostCavern]
  Version: 2.51.1.80702
   Region: US
Game Time: 00:17:25
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Blue (Winner)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-] Apple#1111             [Level:897]   [Toon:1-Hero-1-123456789]
    Hero: Rexxar           [Level:Auto]  [ID:HeroRexxar]
    Award: MVP
[-] Banana#2222            [Level:402]   [Toon:1-Hero-1-987654321]
    Hero: Ragnaros         [Level:Auto]  [ID:HeroRagnaros]
[-] Grapes#3333            [Level:2585]  [Toon:1-Hero-1-111111111]
    Hero: Deckard          [Level:Auto]  [ID:HeroDeckard]
    Award: MostHealing
[-] Mango#4444             [Level:309]   [Toon:1-Hero-1-000000000]
    Hero: Kael'thas        [Level:Auto]  [ID:HeroKaelthas]
[-] Orange#5555            [Level:311]   [Toon:1-Hero-1-121212123]
    Hero: Jaina            [Level:Auto]  [ID:HeroJaina]
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Team Red
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[-] Grapefruit#6666        [Level:1071]  [Toon:1-Hero-1-23232323]
    Hero: Arthas           [Level:Auto]  [ID:HeroArthas]
    Award: MostRoots
[5] Avacado#7777           [Level:1026]  [Toon:1-Hero-1-34343434]
    Hero: Lunara           [Level:Auto]  [ID:HeroDryad]
[5] Strawberries#8888               [Level:1209]  [Toon:1-Hero-1-45454545]
    Hero: Lunara           [Level:Auto]  [ID:HeroDryad]
    Award: MostTeamfightHeroDamageDone
[-] Lemon#9999             [Level:726]   [Toon:1-Hero-1-67676767]
    Hero: Qhira            [Level:Auto]  [ID:HeroNexusHunter]
[-] Olive#0000             [Level:755]   [Toon:1-Hero-1-78787878]
    Hero: Gul'dan          [Level:Auto]  [ID:HeroGuldan]
    Award: MostHeroDamageDone
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Observers
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
(NONE)
```

**Note: When using command prompt on windows, use double quotes instead of single quote when specifying filepaths.**

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

## Developing
To build and compile the code, it is recommended to use the latest version of [Visual Studio 2019 or Visual Studio Code](https://visualstudio.microsoft.com/downloads/).

Another option is to use the dotnet CLI tools from the [.NET 5.0 SDK](https://dotnet.microsoft.com/download).

The main project is `HeroesDecode.csproj` and the main entry point is `Program.cs`.

## License
[MIT license](/LICENSE)

