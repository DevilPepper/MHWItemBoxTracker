#r "ItemBoxTracker.dll"

using System.IO;

using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

var configuration = "Debug";
if(Args.Count > 0) {
  configuration = Args[0];
}

var schema = JsonSchema.Generate<ItemBoxTrackerConfig>();
File.WriteAllText($"ItemBoxTracker/bin/{configuration}/config.schema.json", schema);
