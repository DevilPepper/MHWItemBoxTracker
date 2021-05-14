#r "ItemBoxTracker.dll"

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using HunterPie.Plugins;
using MHWItemBoxTracker;
using MHWItemBoxTracker.Service;
using Newtonsoft.Json;

var configuration = "Debug";
if (Args.Count > 0) {
  configuration = Args[0];
}

var buildDirectory = $"ItemBoxTracker/bin/{configuration}";
var files = new List<string>() {
    "ItemBoxTracker.dll",
    "Microsoft.Xaml.Behaviors.dll",
};

var main = new Main();
var info = new PluginInformation();
info.Name = main.Name;
info.EntryPoint = "Main.cs";
info.Description = main.Description;
info.Author = "Stuff";
info.Version = typeof(ConfigService).Assembly.GetName().Version.ToString();
info.ReleaseDate = DateTime.Now;
// info.Links = [{"name": "Homepage", "url": "..."}, {"name": "Changelog", "url": "..."}];

var update = new PluginInformation.PluginUpdateInformation();
info.Update = update;

update.MinimumVersion = "1.0.5.0";
update.UpdateUrl = "https://github.com/Stuff-Mods/MHWItemBoxTracker/releases/latest/download/";

var hashes = new Dictionary<string, string>();
update.FileHashes = hashes;

using (var sha = SHA256.Create()) {
  foreach (string file in files) {
    var stream = File.Open($"{buildDirectory}/{file}", FileMode.Open);
    stream.Position = 0;

    var hashBytes = sha.ComputeHash(stream);
    stream.Close();

    var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    hashes.Add(file, hash);
  }
}
hashes.Add(ConfigService.settings, "InstallOnly");

var json = JsonConvert.SerializeObject(info, Formatting.Indented);
File.WriteAllText($"{buildDirectory}/module.json", json);
