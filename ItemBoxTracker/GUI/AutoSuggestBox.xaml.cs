using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MHWItemBoxTracker.GUI {
  public partial class AutoSuggestBox : UserControl {
    public ObservableCollection<object> Suggestions { get; } = new ObservableCollection<object>();
    public AutoSuggestBox() {
      InitializeComponent();
      suggestionsList.ItemsSource = Suggestions;
    }

    private void TextChanged(object sender, TextChangedEventArgs e) {
      if (string.IsNullOrEmpty(searchInput.Text)) {
        suggestionsPopup.IsOpen = false;
      } else {
        suggestionsPopup.IsOpen = true;
        OnTextChanged(Suggestions, searchInput.Text);
      }
      noResults.Visibility = Bool2Visibility(Suggestions.Count == 0);
      suggestionsList.Visibility = Bool2Visibility(Suggestions.Count > 0);
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
      suggestionsPopup.IsOpen = false;
      if (suggestionsList.SelectedIndex >= 0) {
        DisplaySelection();
      }
    }
    private void ClearSelection(object sender, RoutedEventArgs e) {
      selectionCtrl.Visibility = Visibility.Collapsed;
      OnClearSelection(Selection);
      searchInput.IsEnabled = true;
      searchInput.Focus();
    }
    public void OnEnter(string input) {
      if (!string.IsNullOrEmpty(input)) {
        OnTextChanged(Suggestions, input);
        if (Suggestions.Count > 0) {
          suggestionsPopup.IsOpen = false;
          suggestionsList.SelectedIndex = 0;
          DisplaySelection();
        }
        noResults.Visibility = Bool2Visibility(Suggestions.Count == 0);
        suggestionsList.Visibility = Bool2Visibility(Suggestions.Count > 0);
      }
    }

    private void DisplaySelection() {
      selectionCtrl.Visibility = Visibility.Visible;
      OnSuggestionChosen(Selection, suggestionsList.SelectedItem);
      suggestionsList.SelectedIndex = -1;

      searchInput.Clear();
      searchInput.IsEnabled = false;
    }

    private Visibility Bool2Visibility(bool isVisible) {
      return isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    public event Action<ObservableCollection<object>, string> OnTextChanged = (l, s) => { };
    public event Action<object> OnClearSelection = o => { };
    // TODO Both are same type
    public event Action<object, object> OnSuggestionChosen = (o, l) => { };

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
    "Placeholder", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata("Search..."));

    public string Placeholder {
      get { return (string)GetValue(PlaceholderProperty); }
      set { SetValue(PlaceholderProperty, value); }
    }
    public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
    "Selection", typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(null));

    public object Selection {
      get { return (object)GetValue(SelectionProperty); }
      set { SetValue(SelectionProperty, value); }
    }
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
    "ItemTemplate", typeof(DataTemplate), typeof(AutoSuggestBox), new PropertyMetadata(default(DataTemplate)));

    public string ItemTemplate {
      get { return (string)GetValue(ItemTemplateProperty); }
      set { SetValue(ItemTemplateProperty, value); }
    }
  }
}
