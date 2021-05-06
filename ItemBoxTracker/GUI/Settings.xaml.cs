using System.Windows.Controls;
using HunterPie.Plugins;
using HunterPie.Settings;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.ViewModels;
using Newtonsoft.Json;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.GUI {
  public partial class Settings : UserControl, ISettings {
    public bool IsSettingsChanged => true;
    private ItemBoxTrackerViewModel vm;

    public Settings() : base() {
      InitializeComponent();
      vm = new ItemBoxTrackerViewModel(new ItemBoxTrackerConfig());
      DataContext = vm;
    }

    public void LoadSettings() {
      Plugin.Log($"Loading Settings: {JsonConvert.SerializeObject(Plugin.Config, Formatting.Indented)}");
      vm = new ItemBoxTrackerViewModel(Plugin.Config);
      DataContext = vm;
    }

    public void SaveSettings() {
      Plugin.Log($"Saving Settings: {JsonConvert.SerializeObject(vm.ToConfig(), Formatting.Indented)}");
      Plugin.Config = vm.ToConfig();
      Dispatch(async () => await Plugin.SaveJson(settings, Plugin.Config));
    }

    public string ValidateSettings() {
      return null;
    }
  }
}
