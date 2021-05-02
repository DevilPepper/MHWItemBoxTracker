using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HunterPie.Plugins;
using HunterPie.UI.Infrastructure;
using HunterPie.Settings;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.ViewModels;
using static MHWItemBoxTracker.Main;
using Newtonsoft.Json;

namespace MHWItemBoxTracker.GUI
{
  public partial class Settings : UserControl, ISettings {
        
    public static string fileName = "settings.json";
    public bool IsSettingsChanged => true;
        
    private ItemBoxTrackerViewModel vm;

    public Settings() : base()
    {
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
