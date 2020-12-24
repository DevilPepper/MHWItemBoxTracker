using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Config
{
    class ConfigLoader
    {
        public static ItemBoxTrackerConfig loadConfig()
        {
            return PathFinder.loadJson<ItemBoxTrackerConfig>("plugin.settings.json");
        }

        public static void saveConfig(ItemBoxTrackerConfig config)
        {
            PathFinder.saveJson("plugin.settings.json", config);
        }
    }
}
