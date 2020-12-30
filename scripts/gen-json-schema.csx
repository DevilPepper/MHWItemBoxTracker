#r "MHWItemBoxTracker.dll"

using System.IO;

using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

var schema = JsonSchema.Generate<ItemBoxTrackerConfig>();
File.WriteAllText("MHWItemBoxTracker/bin/Release/config.schema.json", schema);
