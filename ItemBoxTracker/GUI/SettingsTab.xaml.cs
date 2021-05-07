using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HunterPie.Core.Native;
using HunterPie.Plugins;
using HunterPie.Utils;
using MHWItemBoxTracker.ViewModels;
using Newtonsoft.Json;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.GUI {
  public partial class SettingsTab : UserControl {
    public SettingsTab() : base() {
      InitializeComponent();
    }

    private void DeleteRow(object sender, RoutedEventArgs e) {
      var item = ((FrameworkElement)sender).DataContext as ItemViewModel;
      var Tracking = ((TrackingTabViewModel)DataContext).Tracking;
      Tracking.Remove(item);
      if (Tracking.Count == 0) {
        Tracking.Add(new ItemViewModel());
      }
    }
    private void AddRow(object sender, RoutedEventArgs e) {
      ((TrackingTabViewModel)DataContext).Tracking.Add(new ItemViewModel());
    }
    private void MoveUp(object sender, RoutedEventArgs e) {
      var item = ((FrameworkElement)sender).DataContext as ItemViewModel;
      var Tracking = ((TrackingTabViewModel)DataContext).Tracking;
      var index = Tracking.IndexOf(item);
      if (index > 0) {
        Tracking.Move(index, index - 1);
      }
    }

    private void MoveDown(object sender, RoutedEventArgs e) {
      var item = ((FrameworkElement)sender).DataContext as ItemViewModel;
      var Tracking = ((TrackingTabViewModel)DataContext).Tracking;
      var index = Tracking.IndexOf(item);
      if (index < Tracking.Count - 1) {
        Tracking.Move(index, index + 1);
      }
    }

    private void OnTextChanged(ObservableCollection<object> suggestions, string input) {
      var DC = DataContext as TrackingTabViewModel;
      Plugin.Log($"{DC.Items}");
      Plugin.Log($"{DC.Items.Count}");
      var items = DC.Items
        .Where(
          item => CultureInfo.CurrentCulture.CompareInfo
            .IndexOf(item.Name, input, CompareOptions.IgnoreCase) >= 0)
        .Where(item => !DC.Tracking.Contains(item));

      suggestions.Clear();
      foreach (var item in items) {
        suggestions.Add(item);
      }
    }
    private void OnSuggestionChosen(object selection, object choice) {
      var Selection = (ItemViewModel)selection;
      var Choice = (ItemViewModel)choice;
      Selection.ItemId = Choice.ItemId;
      Selection.Name = Choice.Name;
    }
    private void OnClearSelection(object selection) {
      var Selection = (ItemViewModel)selection;
      Selection.ItemId = 0;
    }
  }
}
