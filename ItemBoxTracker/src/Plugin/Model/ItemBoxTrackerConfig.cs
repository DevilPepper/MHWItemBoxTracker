using System.ComponentModel;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class ItemBoxTrackerConfig : NotifyPropertyChanged {
    private TrackingTabConfig always = new();
    private TrackingTabConfig village = new();
    private TrackingTabConfig quest = new();

    [DisplayName("Track Always")]
    [Description("Items to track everywhere")]
    public TrackingTabConfig Always {
      get => always;
      set => SetField(ref always, value);
    }

    [DisplayName("Track in Village")]
    [Description("Items to track only in the village/gathering hub")]
    public TrackingTabConfig Village {
      get => village;
      set => SetField(ref village, value);
    }

    [DisplayName("Track in Quest")]
    [Description("Items to track during a quest/expedition/guiding lands")]
    public TrackingTabConfig Quest {
      get => quest;
      set => SetField(ref quest, value);
    }
  }
}
