#r "..\MHWItemBoxTracker\bin\Release\HunterPie.Core.dll"
#r "..\MHWItemBoxTracker\bin\Release\Newtonsoft.Json.dll"

using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using HunterPie.Plugins;
using Newtonsoft.Json;

var releaseDirectory = "MHWItemBoxTracker/bin/Release";
var files = new List<string>() {
    "MHWItemBoxTracker.dll",
    "config.schema.json",
};

var info = new PluginInformation();
info.Name = "MHWItemBoxTracker";
info.EntryPoint = "Main.cs";
info.Description = "A HunterPie plugin to track items the player is farming";
info.Author = "Stuff";
info.Version = Args[0];

var update = new PluginInformation.PluginUpdateInformation();
info.Update = update;

update.MinimumVersion = "1.0.4.0";
update.UpdateUrl = "https://github.com/SupaStuff/MHWItemBoxTracker/releases/latest/download/";

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

var json = JsonConvert.SerializeObject(info, Newtonsoft.Json.Formatting.Indented);
File.WriteAllText($"{releaseDirectory}/module.json", json);
