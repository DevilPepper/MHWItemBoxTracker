using System.ComponentModel;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class SettingsModel : NotifyPropertyChanged {
    private TrackingTabModel always = new();
    private TrackingTabModel village = new();
    private TrackingTabModel quest = new();

    [DisplayName("Track Always")]
    [Description("Items to track everywhere")]
    public TrackingTabModel Always {
      get => always;
      set => SetField(ref always, value);
    }

    [DisplayName("Track in Village")]
    [Description("Items to track only in the village/gathering hub")]
    public TrackingTabModel Village {
      get => village;
      set => SetField(ref village, value);
    }

    [DisplayName("Track in Quest")]
    [Description("Items to track during a quest/expedition/guiding lands")]
    public TrackingTabModel Quest {
      get => quest;
      set => SetField(ref quest, value);
    }
  }
}
