using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
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
      ((TrackingTabViewModel)DataContext).Tracking.Remove(item);
    }
    private void AddRow(object sender, RoutedEventArgs e) {
      ((TrackingTabViewModel)DataContext).Tracking.Add(new ItemViewModel());
    }

    private void OnTextChanged(ObservableCollection<object> suggestions, string input) {
      // TODO: Filter items from HunterPie
      suggestions.Clear();
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
