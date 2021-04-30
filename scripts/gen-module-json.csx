#r "ItemBoxTracker.dll"

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using HunterPie.Plugins;
using MHWItemBoxTracker.GUI;
using Newtonsoft.Json;

var configuration = "Debug";
if(Args.Count > 0) {
  configuration = Args[0];
}

var releaseDirectory = $"ItemBoxTracker/bin/{configuration}";
var files = new List<string>() {
    "ItemBoxTracker.dll",
    "config.schema.json",
};

var info = new PluginInformation();
info.Name = "ItemBoxTracker";
info.EntryPoint = "Main.cs";
info.Description = "A HunterPie plugin to track items the player is farming";
info.Author = "Stuff";
info.Version = typeof(Settings).Assembly.GetName().Version.ToString();
info.ReleaseDate = DateTime.Now;
// info.Links = [{"name": "Homepage", "url": "..."}, {"name": "Changelog", "url": "..."}];

var update = new PluginInformation.PluginUpdateInformation();
info.Update = update;

update.MinimumVersion = "1.0.5.0";
update.UpdateUrl = "https://github.com/Stuff-Mods/MHWItemBoxTracker/releases/latest/download/";

var hashes = new Dictionary<string, string>();
update.FileHashes = hashes;

using (var sha = SHA256.Create()){
    foreach (string file in files) {
        var stream = File.Open($"{releaseDirectory}/{file}", FileMode.Open);
        stream.Position = 0;

        var hashBytes = sha.ComputeHash(stream);
        stream.Close();

        var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        hashes.Add(file, hash);
    }
}
hashes.Add(Settings.fileName, "InstallOnly");

var json = JsonConvert.SerializeObject(info, Newtonsoft.Json.Formatting.Indented);
File.WriteAllText($"{releaseDirectory}/module.json", json);
