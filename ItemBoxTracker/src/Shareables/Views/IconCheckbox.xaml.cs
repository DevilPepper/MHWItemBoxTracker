using System.Windows;
using System.Windows.Controls;
using HunterPie.GUI.Widgets.Abnormality_Widget.Parts;

namespace MHWItemBoxTracker.Views {
  public partial class IconCheckbox : UserControl {

    public Image Icon {
      get => (Image)GetValue(ImageProperty);
      set => SetValue(ImageProperty, value);
    }
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register("Icon", typeof(Image), typeof(IconCheckbox));

    public string Text {
      get => (string)GetValue(TextProperty);
      set => SetValue(TextProperty, value);
    }
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(IconCheckbox));

    public IconCheckbox() => InitializeComponent();

    private void OnLoaded(object sender, RoutedEventArgs e) {
      Icon.Width = 32;
      Icon.Height = 32;

      AbnormalitySettingControl settingsControl = new();
      settingsControl.SetAbnormalityInfo(Icon.Source, Text, "007", true);
      panel.Children.Add(settingsControl);
    }
  }
}
