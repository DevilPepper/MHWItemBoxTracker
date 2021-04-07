using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HunterPie.Plugins;

namespace MHWItemBoxTracker
{
    public static class MainExtension
    {
        public static T LoadJson<T>(this Main plugin, string json) where T : new()
        {
            var jsonPath = Path.Combine(plugin.GetPath(), json);
            try {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
            } catch (FileNotFoundException) {
                return new T();
            }
        }

        public static void SaveJson<T>(this Main plugin, string json, T config)
        {
            var jsonPath = Path.Combine(plugin.GetPath(), json);
            var jsonStr = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonStr);
        }
    }
}
