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
using Debugger = HunterPie.Logger.Debugger;

namespace MHWItemBoxTracker.GUI
{
    public partial class Settings : UserControl, ISettings {
        
        public bool IsSettingsChanged => true;
        public string Title => ":D";
        public int MaxLength => 45;

        public Settings()
        {
            InitializeComponent();
        }

        public void LoadSettings() {
            Debugger.Log("Load Settings, mudda fucca");
        }

        public void SaveSettings() {
            Debugger.Log("Save Settings, mudda fucca");
        }

        public string ValidateSettings() {
            Debugger.Log("Nothing here, mudda fucca");
            return null;
        }
    }
}
