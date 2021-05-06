using System.Windows.Controls;
using HunterPie.Plugins;
using HunterPie.Settings;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Service;
using MHWItemBoxTracker.ViewModels;
using Newtonsoft.Json;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.GUI {
  public partial class Settings : UserControl, ISettings {
    public bool IsSettingsChanged => true;
    private ItemBoxTrackerViewModel vm;
    private ConfigService Config;

    public Settings(ConfigService Config) : base() {
      this.Config = Config;
      InitializeComponent();
      vm = new ItemBoxTrackerViewModel(new ItemBoxTrackerConfig());
      DataContext = vm;
    }

    public async void LoadSettings() {
      var config = await Config.LoadAsync();
      Plugin.Log($"Loading Settings: {JsonConvert.SerializeObject(config, Formatting.Indented)}");
      vm = new ItemBoxTrackerViewModel(config);
      DataContext = vm;
    }

    public async void SaveSettings() {
      Plugin.Log($"Saving Settings: {JsonConvert.SerializeObject(vm.ToConfig(), Formatting.Indented)}");
      await Config.SaveAsync(vm.ToConfig());
    }

    public string ValidateSettings() {
      return null;
    }
  }
}
