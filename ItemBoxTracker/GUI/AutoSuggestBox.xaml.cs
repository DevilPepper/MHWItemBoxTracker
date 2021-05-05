using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HunterPie.Plugins;
using HunterPie.UI;
using HunterPie.UI.Infrastructure;
using MHWItemBoxTracker.Converters;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.GUI {
  public partial class AutoSuggestBox : UserControl {
    /// <summary>
    /// Event triggers when the text in the textbox changes (for every change)
    /// Also triggers when the user submits a search query by hitting Enter.
    /// <param name="suggestions"/> the ObservableCollection to update with new suggestions
    /// <param name="input"/> current user input
    /// <example>
    /// <code>
    /// private void OnTextChanged(ObservableCollection<object> suggestions, string input) {
    ///   var filtered = searchSpace.Where(s => s.Contains(input));
    ///   suggestions.Clear();
    ///   foreach (var choice in filtered) {
    ///     suggestions.Add(choice);
    ///   }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public event Action<ObservableCollection<object>, string> OnTextChanged = (suggestions, input) => { };
    /// <summary>
    /// Event triggers when the user clicks the X on their selection
    /// <param name="selection"/> the selection view model given as <see cref="Selection" />
    /// <example>
    /// <code>
    /// private void OnClearSelection(object selection) {
    ///   var Selection = (MyViewModel)selection;
    ///   makeInvalid(Selection);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public event Action<object> OnClearSelection = selection => { };
    /// <summary>
    /// Event triggers when the user chooses a selection.
    /// Also triggers when the user submits a search query by hitting Enter and there is at least 1 result.
    /// The top result is sent with this event.
    /// <param name="selection"/> the selection view model given as <see cref="Selection" />
    /// <param name="choice"/> the selected list item from the suggestions ObservableCollection. i.e.: the <c>ListItem</c>'s <c>DataContext</c>
    /// <example>
    /// <code>
    /// private void OnSuggestionChosen(object selection, object choice) {
    ///   var Selection = (MyViewModel)selection;
    ///   var Choice = (ProbablyTheSameViewModelButDoesNotHaveToBe)choice;
    ///   DoWhateverYouNeedToMakeSelectionReflectTheChoice(Selection, Choice);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public event Action<object, object> OnSuggestionChosen = (o, l) => { };

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
    "Placeholder", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata("Search..."));
    /// <value>
    /// DependencyProperty <c>Placeholder</c>
    /// is the desired placeholder text that displays in an empty search box.
    /// Defaults to "Search..."
    /// </value>
    public string Placeholder {
      get { return (string)GetValue(PlaceholderProperty); }
      set { SetValue(PlaceholderProperty, value); }
    }
    public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
    "Selection", typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(null));
    /// <value>
    /// DependencyProperty <c>Selection</c>
    /// is the viewmodel that will hold the user's selection.
    /// Default isn't very useful
    /// </value>
    public object Selection {
      get { return (object)GetValue(SelectionProperty); }
      set { SetValue(SelectionProperty, value); }
    }
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
    "ItemTemplate", typeof(DataTemplate), typeof(AutoSuggestBox), new PropertyMetadata(default(DataTemplate)));
    /// <value>
    /// DependencyProperty <c>ItemTemplate</c>
    /// is the <c>DataTemplate</c> used to display the individual search results and auto filled selection.
    /// Default simply uses ToString()
    /// </value>
    public string ItemTemplate {
      get { return (string)GetValue(ItemTemplateProperty); }
      set { SetValue(ItemTemplateProperty, value); }
    }

    public AutoSuggestBox() {
      OnEnter = new RelayCommand(i => onEnter(i as string));
      OnDownKey = new ArglessRelayCommand(onDownKey);
      OnUpKey = new ArglessRelayCommand(onUpKey);
      OnEscape = new ArglessRelayCommand(onEscape);
      InitializeComponent();
      suggestionsList.ItemsSource = Suggestions;
      Suggestions.CollectionChanged += SuggestionsChanged;
    }
    private ObservableCollection<object> Suggestions { get; } = new ObservableCollection<object>();
    private bool itIsI = false;

    private void TextChanged(object sender, TextChangedEventArgs e) {
      if (string.IsNullOrEmpty(searchInput.Text)) {
        suggestionsPopup.IsOpen = false;
      } else {
        suggestionsPopup.IsOpen = true;
        OnTextChanged(Suggestions, searchInput.Text);
      }
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
      if (!itIsI && suggestionsList.SelectedIndex >= 0) {
        DisplaySelection();
      }
      itIsI = false;
    }
    private void ClearSelection(object sender, RoutedEventArgs e) {
      OnClearSelection(Selection);
      searchInput.IsEnabled = true;
      searchInput.Focus();
    }

    public ICommand OnEnter { get; }
    public ICommand OnDownKey { get; }
    public ICommand OnUpKey { get; }
    public ICommand OnEscape { get; }
    private void onEnter(string input) {
      if (suggestionsList.SelectedIndex >= 0) {
        DisplaySelection();
      } else if (!string.IsNullOrEmpty(input)) {
        OnTextChanged(Suggestions, input);
        if (Suggestions.Count > 0) {
          // triggers OnSelectionChanged, so no need to call DisplaySelection()
          suggestionsList.SelectedIndex = 0;
        }
      }
    }
    private void onDownKey() {
      var count = Suggestions.Count;
      if (count > 0 && suggestionsList.SelectedIndex < (count - 1)) {
        itIsI = true;
        suggestionsList.SelectedIndex++;
      }
    }
    private void onUpKey() {
      if (Suggestions.Count > 0 && suggestionsList.SelectedIndex > -1) {
        itIsI = true;
        suggestionsList.SelectedIndex--;
      }
    }
    private void onEscape() {
      suggestionsPopup.IsOpen = false;
    }

    private void OnLoaded(object sender, RoutedEventArgs e) {
      var empty = Activator.CreateInstance(Selection.GetType());
      if (!empty.Equals(Selection)) {
        searchInput.IsEnabled = false;
      }
    }

    void SuggestionsChanged(object sender, NotifyCollectionChangedEventArgs e) {
      var isEmpty = Suggestions.Count == 0;
      suggestionsList.Visibility = isEmpty ? Visibility.Collapsed : Visibility.Visible;
      noResults.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
    }
    private void DisplaySelection() {
      suggestionsPopup.IsOpen = false;
      OnSuggestionChosen(Selection, suggestionsList.SelectedItem);
      suggestionsList.SelectedIndex = -1;

      searchInput.IsEnabled = false;
      searchInput.Clear();
    }
  }
}
