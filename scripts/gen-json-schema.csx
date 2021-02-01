#r "ItemBoxTracker.dll"

using System.IO;

using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

var schema = JsonSchema.Generate<ItemBoxTrackerConfig>();
File.WriteAllText("ItemBoxTracker/bin/Release/config.schema.json", schema);
