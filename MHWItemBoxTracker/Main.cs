using System.IO;
using Newtonsoft.Json;
using HunterPie.Core;
using HunterPie.Plugins;
using MHWItemBoxTracker.helper;
using System.Reflection;

namespace MHWItemBoxTracker
{
    public class Main : IPlugin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Game Context { get; set; }
        private ItemBoxTracker tracker { get; set; }
        public void Initialize(Game context)
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var modulePath = Path.Combine(assemblyFolder, "module.json");
            var module = JsonConvert.DeserializeObject<PluginInformation>(File.ReadAllText(modulePath));
            
            Name = module.Name;
            Description = module.Description;
            Context = context;

            tracker = new ItemBoxTracker(context.Player);
            tracker.hookEvents();
        }

        public void Unload()
        {
            tracker.unhookEvents();
        }
    }
}