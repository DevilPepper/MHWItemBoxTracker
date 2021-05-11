using System.Collections.ObjectModel;
using System.Linq;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels {
  public class TrackingTabViewModel : NotifyPropertyChanged {
    private bool trackPouch;
    private bool trackBox;
    private bool trackCraftable;
    private ObservableCollection<ItemViewModel> tracking;
    private ObservableCollection<ItemViewModel> items;

    public TrackingTabViewModel(TrackingTabConfig config) {
      trackPouch = config.TrackPouch;
      trackBox = config.TrackBox;
      trackCraftable = config.TrackCraftable;
      tracking = new ObservableCollection<ItemViewModel>(
        config.Tracking
              .Select(i => new ItemViewModel(i))
              .ToList()
        );

      if (tracking.Count == 0) {
        tracking.Add(new ItemViewModel());
      }
    }

    public bool TrackPouch {
      get => trackPouch;
      set => SetField(ref trackPouch, value);
    }

    public bool TrackBox {
      get => trackBox;
      set => SetField(ref trackBox, value);
    }

    public bool TrackCraftable {
      get => trackCraftable;
      set => SetField(ref trackCraftable, value);
    }

    public ObservableCollection<ItemViewModel> Tracking {
      get => tracking;
      set => SetField(ref tracking, value);
    }

    public ObservableCollection<ItemViewModel> Items {
      get => items;
      set => SetField(ref items, value);
    }

    public TrackingTabConfig ToConfig() {
      return new TrackingTabConfig {
        TrackPouch = trackPouch,
        TrackBox = trackBox,
        TrackCraftable = trackCraftable,
        Tracking = tracking.Select(i => i.ToConfig())
                           .Where(i => i.ItemId > 0)
                           .ToList()
      };
    }
  }
}
