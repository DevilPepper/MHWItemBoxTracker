using System.Windows.Controls;
using HunterPie.Plugins;
using HunterPie.Settings;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.ViewModels;
using Newtonsoft.Json;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.GUI {
  public partial class Settings : UserControl, ISettings {

    public static string fileName = "settings.json";
    public bool IsSettingsChanged => true;

    private readonly ItemBoxTrackerViewModel vm;

    public Settings() : base() {
      InitializeComponent();
      vm = new ItemBoxTrackerViewModel(new ItemBoxTrackerConfig());
      DataContext = vm;
    }

    public void LoadSettings() {
      Plugin.Log($"Loading Settings: {JsonConvert.SerializeObject(vm.ToConfig(), Formatting.Indented)}");
    }

    public void SaveSettings() {
      Plugin.Log($"Saving Settings: {JsonConvert.SerializeObject(vm.ToConfig(), Formatting.Indented)}");
    }

    public string ValidateSettings() {
      return null;
    }
  }
}
