# Monster Hunter World Item Box Tracker

A plugin for hunter pie that lets players track the items they are farming.

Reducing cognitive overhead by allowing the player to stop looking at the box after every quest to find out that they still need 5 more tickets, 1 less than the last time they checked, and stop asking "how many did I need again?"

## Usage

Come back later.

## Install

### Requirements

- [Hunter Pie >= 1.0.3.99](https://github.com/Haato3o/HunterPie)
- [.NET Framework >= 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

### Setup

1. [module.json]()
   - Drag this link into the Hunter Pie window
   - OR download it to `HunterPie/Modules/ItemBoxTracker`
2. Let Hunter Pie do its thing

## Build

Requires [HunterPie.Core.dll](https://github.com/ForksKnivesAndSpoons/HunterPie/releases/latest/download/HunterPie.Core.dll).
You can download it or grab it from the HunterPie Libs directory (>= 1.0.3.99) and throw it in the project root. Even better, from the project root run:

```bash
curl -L https://github.com/ForksKnivesAndSpoons/HunterPie/releases/latest/download/HunterPie.Core.dll -o HunterPie.Core.dll
```

Then to build it:

```bash
msbuild MHWItemBoxTracker.sln -m /t:Restore
msbuild MHWItemBoxTracker.sln -m /p:Configuration=Release
```
