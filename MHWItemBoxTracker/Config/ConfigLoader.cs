using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Config
{
    class ConfigLoader
    {
        public static ItemBoxTrackerConfig loadConfig()
        {
            return PathFinder.loadJson<ItemBoxTrackerConfig>("plugin.settings.json");
        }
    }
}
