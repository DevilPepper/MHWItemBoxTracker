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

namespace MHWItemBoxTracker.GUI
{
  public partial class Settings : UserControl, ISettings {
        
    public static string fileName = "settings.json";
    public bool IsSettingsChanged => true;
        
    private ItemBoxTrackerViewModel vm;

    public Settings()
    {
      InitializeComponent();
      vm = new ItemBoxTrackerViewModel(new ItemBoxTrackerConfig());
      this.DataContext = vm;
    }

    public void LoadSettings() {
    }

    public void SaveSettings() {
    }

    public string ValidateSettings() {
      return null;
    }
  }
}
