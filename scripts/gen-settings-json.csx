#r "ItemBoxTracker.dll"

using System.IO;

using MHWItemBoxTracker.GUI;
using MHWItemBoxTracker.Config;
using Newtonsoft.Json;

// TODO: D.R.Y.
var configuration = "Debug";
if(Args.Count > 0) {
  configuration = Args[0];
}

var releaseDirectory = $"ItemBoxTracker/bin/{configuration}";

var settings = new ItemBoxTrackerConfig();
var json = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);
File.WriteAllText($"{releaseDirectory}/{Settings.fileName}", json);
