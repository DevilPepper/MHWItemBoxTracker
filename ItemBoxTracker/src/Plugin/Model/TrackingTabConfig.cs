using System.Collections.Generic;
using System.ComponentModel;

namespace MHWItemBoxTracker.Model {
  public class TrackingTabConfig {
    [DisplayName("Track items in pouch")]
    [Description("Enable to count items in your pouch on the progress bars")]
    public bool TrackPouch { get; set; } = false;

    [DisplayName("Track items in box")]
    [Description("Enable to count items in your box on the progress bars")]
    public bool TrackBox { get; set; } = true;

    [DisplayName("Track craftable items")]
    [Description("Enable to count items that can be crafted on the progress bars")]
    public bool TrackCraftable { get; set; } = false;

    [DisplayName("Items to Track")]
    [Description("You can find item IDs here: https://github.com/Ezekial711/MonsterHunterWorldModding/wiki/Item-IDs")]
    public List<ItemConfig> Tracking { get; set; } = new List<ItemConfig>();
  }
}
