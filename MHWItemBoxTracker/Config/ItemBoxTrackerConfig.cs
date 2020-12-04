using System.Collections.Generic;
using HunterPie.Plugins;

namespace MHWItemBoxTracker.Config
{
    class ItemBoxTrackerConfig : PluginSettings
    {
        public List<ItemConfig> tracking { get; set; }
    }
}
