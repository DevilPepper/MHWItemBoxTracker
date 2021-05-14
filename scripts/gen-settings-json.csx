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

var buildDirectory = $"ItemBoxTracker/bin/{configuration}";

var settings = new SettingsModel();
var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
File.WriteAllText($"{buildDirectory}/{ConfigService.settings}", json);
