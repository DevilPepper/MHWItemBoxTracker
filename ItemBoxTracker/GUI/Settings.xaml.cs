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
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.GUI
{
  public partial class Settings : UserControl, ISettings {
        
    public static string fileName = "settings.json";
    public bool IsSettingsChanged => true;
        
    private ItemBoxTrackerConfig Config = new ItemBoxTrackerConfig();

    public Settings()
    {
      InitializeComponent();
      this.DataContext = Config;
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
