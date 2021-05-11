using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MHWItemBoxTracker.ViewModels;

namespace MHWItemBoxTracker.GUI {
  public partial class AutoSuggestBox : UserControl {
    public AutoSuggestBoxViewModel VM { get; }
    public AutoSuggestBox() {
      InitializeComponent();
      VM = new AutoSuggestBoxViewModel(
        (suggestions, input) => OnTextChanged(suggestions, input),
        (selection, selectedItem) => OnSuggestionChosen(selection, selectedItem)
      );
      Root.DataContext = VM;
    }

    // Couldn't get away from this :(
    private void ClearSelection(object sender, RoutedEventArgs e) {
      OnClearSelection(Selection);
      searchInput.IsEnabled = true;
      searchInput.Focus();
    }
    private void OnLoaded(object sender, RoutedEventArgs e) {
      var empty = Activator.CreateInstance(Selection.GetType());
      if (!empty.Equals(Selection)) {
        searchInput.IsEnabled = false;
      } else {
        searchInput.Focus();
      }
    }

    private void SelectionChanged(object sender, RoutedEventArgs e) {
      var theList = sender as ListBox;
      theList.ScrollIntoView(theList.SelectedItem);
    }

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
    public event Action<object, object> OnSuggestionChosen = (selection, selectedItem) => { };

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

    public static readonly DependencyProperty NoResultsProperty = DependencyProperty.Register(
    "NoResultsText", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata("No Results..."));
    /// <value>
    /// DependencyProperty <c>NoResultsText</c>
    /// is the text to show when there are no results.
    /// Defaults to "No Results..."
    /// </value>
    public string NoResultsText {
      get { return (string)GetValue(NoResultsProperty); }
      set { SetValue(NoResultsProperty, value); }
    }
    public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
    "Selection", typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(null, (d, e) => {
      var control = d as AutoSuggestBox;
      control.VM.Selection = e.NewValue;
    }));
    /// <value>
    /// DependencyProperty <c>Selection</c>
    /// is the viewmodel that will hold the user's selection.
    /// Default isn't very useful
    /// </value>
    public object Selection {
      get { return (object)GetValue(SelectionProperty); }
      set { SetValue(SelectionProperty, value); }
    }
  }
}
