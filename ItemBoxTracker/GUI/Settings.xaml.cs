using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HunterPie.Core.Native;
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
      vm = new ItemBoxTrackerViewModel(config);
      DataContext = vm;
    }

    public async void SaveSettings() {
      await Config.SaveAsync(vm.ToConfig());
    }

    public string ValidateSettings() {
      return null;
    }

    private void OnLoaded(object sender, RoutedEventArgs e) {
      LoadItems();
    }

    private void LoadItems() {
      var items = new ObservableCollection<ItemViewModel>(Enumerable
        .Range(0, GMD.Items.gValuesOffsets.Length / 2)
        .Select(id => new ItemViewModel {
          ItemId = id,
          Name = GMD.GetItemNameById(id)
        })
        .Where(item => item.Name != null)
        .ToList());

      vm.Items.Clear();
      foreach (var item in items) {
        vm.Items.Add(item);
      }
    }
  }
}
