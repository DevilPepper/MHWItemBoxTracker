using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.ViewModels;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.GUI
{
  public partial class SettingsTab : UserControl {
    public SettingsTab()
    {
      InitializeComponent();
      Items2Track.ItemsSource = Context.Tracking;
    }

    public static new readonly DependencyProperty ContextProperty =
      DependencyProperty.Register("Context", typeof(TrackingTabViewModel), typeof(SettingsTab));
    public new TrackingTabViewModel Context
    {
      get => (TrackingTabViewModel)GetValue(ContextProperty);
      set => SetValue(ContextProperty, value);
    }


  }
}
