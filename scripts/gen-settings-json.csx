#r "ItemBoxTracker.dll"

using System.IO;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Service;
using Newtonsoft.Json;

// TODO: D.R.Y.
var configuration = "Debug";
if (Args.Count > 0) {
  configuration = Args[0];
}

var releaseDirectory = $"ItemBoxTracker/bin/{configuration}";

var settings = new ItemBoxTrackerConfig();
var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
File.WriteAllText($"{releaseDirectory}/{ConfigService.settings}", json);
