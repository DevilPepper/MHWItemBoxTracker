#r "MHWItemBoxTracker.dll"

using System.IO;

using MHWItemBoxTracker.Config;
using Newtonsoft.Json;

// TODO: D.R.Y.
var releaseDirectory = "MHWItemBoxTracker/bin/Release";

var settings = new ItemBoxTrackerConfig();
var json = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);
File.WriteAllText($"{releaseDirectory}/{ConfigLoader.settings}", json);
