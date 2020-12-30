# Monster Hunter World Item Box Tracker

A plugin for hunter pie that lets players track the items they are farming.

Reducing cognitive overhead by allowing the player to stop looking at the box after every quest to find out that they still need 5 more tickets, 1 less than the last time they checked, and stop asking "how many did I need again?"

![Bootleg](https://user-images.githubusercontent.com/5027713/103073631-f05cf680-4595-11eb-9fd8-47546db63873.png)

## Install

### Requirements

- [Hunter Pie >= 1.0.4.0](https://github.com/Haato3o/HunterPie)
- [.NET Framework >= 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

### Setup

1. Download [module.json](https://github.com/SupaStuff/MHWItemBoxTracker/releases/latest/download/module.json) to _`<HunterPie>/Modules/ItemBoxTracker`_
   - Alternatively you can extract [ItemBoxTracker.zip](https://github.com/SupaStuff/MHWItemBoxTracker/releases/latest/download/ItemBoxTracker.zip) to the HunterPie directory
2. Let Hunter Pie do its thing
3. To config the tracker, you can go [here](https://json-editor.github.io/json-editor/) or [here](https://rjsf-team.github.io/react-jsonschema-form/) and copy and paste the contents of [config.schema.json](https://github.com/SupaStuff/MHWItemBoxTracker/releases/latest/download/config.schema.json) into the schema box. Then you can copy the output to _Modules/MHWItemBoxTracker/settings.json_
   - They both have a bug where Overlay.Position are invalid if you leave them empty, so at least make both coordinates 0 or something.

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
