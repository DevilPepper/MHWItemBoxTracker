using System.Collections.Generic;
using System.ComponentModel;

namespace MHWItemBoxTracker.Config {
  public class DeprecatedItemBoxTrackerConfig {
    [DisplayName("Items to Track")]
    [Description("You can find item IDs here: https://github.com/Ezekial711/MonsterHunterWorldModding/wiki/Item-IDs")]
    public List<ItemConfig> Tracking { get; set; } = new List<ItemConfig>();
  }
}
