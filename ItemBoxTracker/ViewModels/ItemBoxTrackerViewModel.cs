using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels
{
  public class ItemBoxTrackerViewModel : NotifyPropertyChanged
  {
    private TrackingTabViewModel always;
    private TrackingTabViewModel village;
    private TrackingTabViewModel quest;

    public ItemBoxTrackerViewModel(ItemBoxTrackerConfig config) {
      always = new TrackingTabViewModel(config.Always);
      village = new TrackingTabViewModel(config.Village);
      quest = new TrackingTabViewModel(config.Quest);
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

    public ItemBoxTrackerConfig ToConfig() {
      var config = new ItemBoxTrackerConfig();
      config.Always = always.ToConfig();
      config.Village = village.ToConfig();
      config.Quest = quest.ToConfig();
      return config;
    }
  }
}
