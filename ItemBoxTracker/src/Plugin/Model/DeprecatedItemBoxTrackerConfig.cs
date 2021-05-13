using System.Collections.Generic;
using System.ComponentModel;

namespace MHWItemBoxTracker.Model {
  public class DeprecatedItemBoxTrackerConfig {
    [DisplayName("Items to Track")]
    [Description("You can find item IDs here: https://github.com/Ezekial711/MonsterHunterWorldModding/wiki/Item-IDs")]
    public List<ItemModel> Tracking { get; set; } = new List<ItemModel>();
  }
}
