using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HunterPie.Core.Native;
using HunterPie.Plugins;
using HunterPie.UI.Infrastructure;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.ViewModel {
  public class SettingsViewModel : NotifyPropertyChanged {
    private TrackingTabViewModel always;
    private TrackingTabViewModel village;
    private TrackingTabViewModel quest;
    private ObservableCollection<ItemConfig> items = new();

    public SettingsViewModel(ItemBoxTrackerConfig Config) {
      this.Config = Config;
      LoadItems = new ArglessRelayCommand(LoadItemsFromGMD);

      always = new TrackingTabViewModel(Config.Always) {
        Items = Items
      };
      village = new TrackingTabViewModel(Config.Village) {
        Items = Items
      };
      quest = new TrackingTabViewModel(Config.Quest) {
        Items = Items
      };
    }
    public ItemBoxTrackerConfig Config { get; }
    public ICommand LoadItems { get; }

    private void LoadItemsFromGMD() {
      if (GMD.Items.gValuesOffsets != null) {
        var items = new ObservableCollection<ItemConfig>(Enumerable
          .Range(0, GMD.Items.gValuesOffsets.Length / 2)
          .Select(id => new ItemConfig {
            ItemId = id,
            Name = GMD.GetItemNameById(id)
          })
          .Where(item => item.Name != null)
          .ToList());

        // TODO: Try setting it instead
        Items.Clear();
        foreach (var item in items) {
          Items.Add(item);
        }
      }
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

    public ObservableCollection<ItemConfig> Items {
      get => items;
      set => SetField(ref items, value);
    }
  }
}
