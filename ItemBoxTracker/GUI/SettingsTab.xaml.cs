using System.Windows;
using System.Windows.Controls;
using MHWItemBoxTracker.ViewModels;

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
  }
}
