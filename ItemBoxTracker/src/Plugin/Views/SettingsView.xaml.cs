using System.Windows.Controls;
using HunterPie.Settings;
using MHWItemBoxTracker.Service;
using MHWItemBoxTracker.ViewModel;

namespace MHWItemBoxTracker.Views {
  public partial class SettingsView : UserControl, ISettings {
    public bool IsSettingsChanged => true;
    private SettingsViewModel VM;
    private ConfigService Config;

    public SettingsView(ConfigService Config) : base() {
      this.Config = Config;
      InitializeComponent();
      VM = new(new());
      DataContext = VM;
    }

    public async void LoadSettings() {
      var config = await Config.LoadAsync();
      VM = new(config);
      DataContext = VM;
    }

    public async void SaveSettings() {
      await Config.SaveAsync();
      // TODO: Inventory.Refresh()
    }

    public string ValidateSettings() {
      return null;
    }
  }
}
