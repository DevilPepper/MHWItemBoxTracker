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
        public static async Task<T> LoadJson<T>(this Main plugin, string json) where T : new()
        {
            var jsonPath = Path.Combine(plugin.GetPath(), json);
            try {
                using (var reader = new StreamReader(jsonPath))
                {
                    return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync()) ?? new T();
                }
            } catch (FileNotFoundException) {
                return new T();
            }
        }

        public static async Task SaveJson<T>(this Main plugin, string json, T config)
        {
            var jsonPath = Path.Combine(plugin.GetPath(), json);
            var jsonStr = JsonConvert.SerializeObject(config, Formatting.Indented);
            using (var writer = new StreamWriter(jsonPath))
            {
                plugin.Log($"Writing to {jsonPath}: {jsonStr}");
                await writer.WriteAsync(jsonStr);
            }
            return;
        }
    }
}
