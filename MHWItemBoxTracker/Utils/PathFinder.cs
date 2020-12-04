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

        public static T loadJson<T>(string json)
        {
            var jsonPath = Path.Combine(PathFinder.getPluginPath(), json);
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
        }
    }
}
