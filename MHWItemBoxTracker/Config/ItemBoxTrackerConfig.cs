using System.Collections.Generic;
using HunterPie.Plugins;

namespace MHWItemBoxTracker.Config
{
    class ItemBoxTrackerConfig : PluginSettings
    {
        public List<ItemConfig> tracking { get; set; } = new List<ItemConfig>();
        public ItemBoxTrackerOverlayConfig overlay { get; set; } = new ItemBoxTrackerOverlayConfig();
    }

    class ItemBoxTrackerOverlayConfig : HunterPie.Core.UserSettings.Config.IWidgetSettings
    {
        public bool Initialize { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public int[] Position { get; set; } = new int[] { 20, 20 };
        public float Opacity { get; set; } = 1;
        public double Scale { get; set; } = 1;
    }
}
