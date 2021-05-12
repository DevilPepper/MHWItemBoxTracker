using System.Windows;
using System.Windows.Controls;
using MHWItemBoxTracker.ViewModel;

namespace MHWItemBoxTracker.Views {
  public partial class AutoSuggestBox : UserControl {
    public AutoSuggestBox() {
      InitializeComponent();
    }

    private void SelectionChanged(object sender, RoutedEventArgs e) {
      var theList = sender as ListBox;
      theList.ScrollIntoView(theList.SelectedItem);
    }

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
    public static readonly DependencyProperty VMProperty = DependencyProperty.Register(
    "VM", typeof(SearchableViewModel), typeof(AutoSuggestBox), new PropertyMetadata(null));
    /// <value>
    /// DependencyProperty <c>VM</c>
    /// is the viewmodel to bind to the searchbox.
    /// Default isn't very useful
    /// See SearchableViewModel
    /// </value>
    public SearchableViewModel VM {
      get { return (SearchableViewModel)GetValue(VMProperty); }
      set { SetValue(VMProperty, value); }
    }
  }
}
