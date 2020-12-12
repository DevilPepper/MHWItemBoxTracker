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
> **TODO: Actually link to module.json**

1. [module.json]()
   - Drag this link into the Hunter Pie window
   - OR download it to `HunterPie/Modules/ItemBoxTracker`
2. Let Hunter Pie do its thing

## Build

> **TODO: Update NuGet source when main HP pushes libs to NuGet with CI**

You'll need to add `ForksKnivesAndSpoons` to your NuGet sources one time:

```bash
nuget sources add -name FKnS -source https://nuget.pkg.github.com/ForksKnivesAndSpoons/index.json -UserName $GITHUB_USERNAME -Password $GITHUB_PASSWORD
```

> If you built HunterPie locally, you can also copy _HunterPie.Core.dll_ to the root of this project and it'll override the NuGet package.

Then to build it:

```bash
msbuild MHWItemBoxTracker.sln -m /t:Restore
msbuild MHWItemBoxTracker.sln -m /p:Configuration=Release
```
