# Monster Hunter World Item Box Tracker

A plugin for hunter pie that lets players track the items they are farming.

Reducing cognitive overhead by allowing the player to stop looking at the box after every quest to find out that they still need 5 more tickets, 1 less than the last time they checked, and stop asking "how many did I need again?"

![Overlay](https://user-images.githubusercontent.com/5027713/118386273-4df4ec80-b5e4-11eb-9a6b-7c1e4c2f9fa2.png)


> The numbers displayed are `pouch | box (+craftable) / wanted`
> 
> Craftable items are counted from box items if **Track items in box** is enabled and from pouch if **Track items in pouch** is enabled. The number shown is their sum.

## Changelog

The changlog is auto generated on the project's wiki from commit messages.
Only releases with notable changes (i.e. that impact the user) are noted in the changelog.
Here's the [Changelog](https://github.com/Stuff-Mods/MHWItemBoxTracker/wiki/Changelog).

## Install

### Requirements

- [Hunter Pie >= 1.0.5.0](https://github.com/Haato3o/HunterPie)
- [.NET Framework >= 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

### Setup

1. Open HunterPie and navigate to _Plugins_ and search for `ItemBoxTracker`. Download and restart.
   - Alternatively you can extract [ItemBoxTracker.zip](https://github.com/Stuff-Mods/MHWItemBoxTracker/releases/latest/download/ItemBoxTracker.zip) to the HunterPie directory, but why would you?
2. In HunterPie, navigate to _Settings->Plugins->ItemBoxTracker_. Should be intuitive.
   - Add an item by typing its name in the search box and selecting it.
   - Amount is disabled until there is an item selected.
   - Hit `Add another` to... add another.

> Note: the switches don't do anything yet and only the **Always** tab is used (only in the village :man_facepalming:).
> These features will be implemented by ![label: enhancements](https://img.shields.io/badge/-enhancement-%23a2eeef) #24, #25, and #26

## Build

> **TODO: Update NuGet source when main HP pushes libs to NuGet with CI**

You'll need to add `ForksKnivesAndSpoons` to your NuGet sources one time:

```bash
nuget sources add -name FKnS -source https://nuget.pkg.github.com/ForksKnivesAndSpoons/index.json -UserName $GITHUB_USERNAME -Password $GITHUB_PASSWORD
```

> If you built HunterPie locally, you can also copy _HunterPie.Core.dll_ and _HunterPie.UI.dll_ to the root of this project and change the `PackageReference`s for the commented out `Reference`s in _ItemBoxTracker/ItemBoxTracker.csproj_.

Then to build it:

```bash
msbuild -m /t:Restore
msbuild -m /p:Configuration=Release
```
