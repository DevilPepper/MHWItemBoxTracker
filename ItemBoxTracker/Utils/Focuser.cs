using System.Windows;
using HunterPie.Plugins;
using Microsoft.Xaml.Behaviors;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Utils {
  public class Focuser : DependencyObject {
    public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.Register(
      "IsFocused", typeof(bool), typeof(Focuser), new PropertyMetadata(false, (d, e) => {
        Plugin.Log("setting focus");
        var ctrl = (UIElement)d;
        if ((bool)e.NewValue) {
          ctrl.Focus();
        }
      }));
    public static bool GetIsFocused(DependencyObject ctrl) => (bool)ctrl.GetValue(IsFocusedProperty);
    public static void SetIsFocused(DependencyObject ctrl, bool value) {
      ctrl.SetValue(IsFocusedProperty, value);
    }
  }
}
