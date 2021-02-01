using HunterPie.Core;
using HunterPie.Plugins;
using MHWItemBoxTracker;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker
{
    public class Main : IPlugin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Game Context { get; set; }
        private Controller.ItemBoxTracker tracker { get; set; }

        public void Initialize(Game context)
        {
            var module = PathFinder.loadJson<PluginInformation>("module.json");

            Name = module.Name;
            Description = module.Description;
            Context = context;

            tracker = new Controller.ItemBoxTracker(context.Player);
            hookEvents();
        }

        public void Unload()
        {
            unhookEvents();
            tracker.unregister();
        }

        internal void hookEvents()
        {
            var player = Context.Player;
            player.OnVillageEnter += tracker.loadItemBox;
            player.OnVillageLeave += tracker.unloadItemBox;
            player.ItemBox.OnItemBoxUpdate += tracker.loadItemBox;

            tracker.loadItemBox();
        }

        internal void unhookEvents()
        {
            var player = Context.Player;
            player.OnVillageEnter -= tracker.loadItemBox;
            player.OnVillageLeave -= tracker.unloadItemBox;
            player.ItemBox.OnItemBoxUpdate -= tracker.loadItemBox;

            tracker.unloadItemBox();
        }
    }
}
