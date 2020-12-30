using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Config
{
    public class ConfigLoader
    {
        public static string settings = "settings.json";
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
