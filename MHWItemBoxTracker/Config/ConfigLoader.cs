using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Config
{
    class ConfigLoader
    {
        private static string settings = "settings.json";
        public static ItemBoxTrackerConfig loadConfig()
        {
            return PathFinder.loadJson<ItemBoxTrackerConfig>(settings);
        }

        public static void saveConfig(ItemBoxTrackerConfig config)
        {
            PathFinder.saveJson(settings, config);
        }
    }
}
