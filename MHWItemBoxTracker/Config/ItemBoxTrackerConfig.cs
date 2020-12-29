using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HunterPie.Plugins;

namespace MHWItemBoxTracker.Config
{
    public class ItemBoxTrackerConfig
    {

        [DisplayName("Items to Track")]
        [Description("You can find item IDs here: https://github.com/Ezekial711/MonsterHunterWorldModding/wiki/Item-IDs")]
        public List<ItemConfig> Tracking { get; set; } = new List<ItemConfig>();

        [DisplayName("Overlay Settings")]
        public ItemBoxTrackerOverlayConfig Overlay { get; set; } = new ItemBoxTrackerOverlayConfig();
    }

    public class ItemBoxTrackerOverlayConfig : HunterPie.Core.UserSettings.Config.IWidgetSettings
    {
        public bool Initialize { get; set; } = true;
        public bool Enabled { get; set; } = true;
        [MaxLength(2)]
        [MinLength(2)]
        [Description("item 1: Left | item 2: Top")]
        public int[] Position { get; set; } = new int[] { 20, 20 };
        public float Opacity { get; set; } = 1;
        public double Scale { get; set; } = 1;
    }
}
