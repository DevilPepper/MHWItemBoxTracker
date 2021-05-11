using System.Collections.ObjectModel;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels {
  public class ItemBoxTrackerViewModel : NotifyPropertyChanged {
    private TrackingTabViewModel always;
    private TrackingTabViewModel village;
    private TrackingTabViewModel quest;
    private ObservableCollection<ItemViewModel> items = new();

    public ItemBoxTrackerViewModel(ItemBoxTrackerConfig config) {
      always = new TrackingTabViewModel(config.Always) {
        Items = Items
      };
      village = new TrackingTabViewModel(config.Village) {
        Items = Items
      };
      quest = new TrackingTabViewModel(config.Quest) {
        Items = Items
      };
    }

    public TrackingTabViewModel Always {
      get => always;
      set => SetField(ref always, value);
    }

    public TrackingTabViewModel Village {
      get => village;
      set => SetField(ref village, value);
    }

    public TrackingTabViewModel Quest {
      get => quest;
      set => SetField(ref quest, value);
    }

    public ObservableCollection<ItemViewModel> Items {
      get => items;
      set => SetField(ref items, value);
    }

    public ItemBoxTrackerConfig ToConfig() {
      return new ItemBoxTrackerConfig {
        Always = always.ToConfig(),
        Village = village.ToConfig(),
        Quest = quest.ToConfig()
      };
    }
  }
}
