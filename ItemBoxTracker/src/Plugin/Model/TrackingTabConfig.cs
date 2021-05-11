using System.Collections.ObjectModel;
using System.ComponentModel;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class TrackingTabConfig : NotifyPropertyChanged {
    private bool trackPouch = false;
    private bool trackBox = true;
    private bool trackCraftable = false;
    private ObservableCollection<ItemConfig> tracking = new();

    [DisplayName("Track items in pouch")]
    [Description("Enable to count items in your pouch on the progress bars")]
    public bool TrackPouch {
      get => trackPouch;
      set => SetField(ref trackPouch, value);
    }

    [DisplayName("Track items in box")]
    [Description("Enable to count items in your box on the progress bars")]
    public bool TrackBox {
      get => trackBox;
      set => SetField(ref trackBox, value);
    }

    [DisplayName("Track craftable items")]
    [Description("Enable to count items that can be crafted on the progress bars")]
    public bool TrackCraftable {
      get => trackCraftable;
      set => SetField(ref trackCraftable, value);
    }

    [DisplayName("Items to Track")]
    public ObservableCollection<ItemConfig> Tracking {
      get => tracking;
      set => SetField(ref tracking, value);
    }
  }
}
