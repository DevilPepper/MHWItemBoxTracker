using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels
{
  public class TrackingTabViewModel : NotifyPropertyChanged
  {
    private bool trackPouch;
    private bool trackBox;
    private bool trackCraftable;
    private ObservableCollection<ItemViewModel> tracking;

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

    public TrackingTabConfig ToConfig() {
      var config = new TrackingTabConfig();
      config.TrackPouch = trackPouch;
      config.TrackBox = trackBox;
      config.TrackCraftable = trackCraftable;
      config.Tracking = tracking.Select(i => i.ToConfig()).ToList();
      return config;
    }
  }
}
