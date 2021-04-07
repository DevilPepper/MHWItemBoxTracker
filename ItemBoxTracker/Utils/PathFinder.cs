using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MHWItemBoxTracker.Utils
{
    class PathFinder
    {
        public static string getPluginPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static T loadJson<T>(string json) where T : new()
        {
            var jsonPath = Path.Combine(PathFinder.getPluginPath(), json);
            try {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
            } catch (FileNotFoundException) {
                return new T();
            }
        }

        public static void saveJson<T>(string json, T config)
        {
            var jsonPath = Path.Combine(PathFinder.getPluginPath(), json);
            var jsonStr = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonStr);
        }
    }
}
