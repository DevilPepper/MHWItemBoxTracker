using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HunterPie.Plugins;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Utils
{
    class PathFinder
    {
        public static T loadJson<T>(string json) where T : new()
        {
            var jsonPath = Path.Combine(Plugin.GetPath(), json);
            try {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
            } catch (FileNotFoundException) {
                return new T();
            }
        }

        public static void saveJson<T>(string json, T config)
        {
            var jsonPath = Path.Combine(Plugin.GetPath(), json);
            var jsonStr = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonStr);
        }
    }
}
