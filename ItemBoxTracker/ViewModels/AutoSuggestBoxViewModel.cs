using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HunterPie.UI.Infrastructure;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels {
  public class AutoSuggestBoxViewModel : NotifyPropertyChanged {
    // From DependencyProperties
    private object selection;
    public object Selection {
      get => selection;
      set => SetField(ref selection, value);
    }
    public Action<ObservableCollection<object>, string> OnTextChanged;
    public Action<object, object> OnSuggestionChosen;
    public AutoSuggestBoxViewModel(Action<ObservableCollection<object>, string> OnTextChanged,
                                   Action<object, object> OnSuggestionChosen) {
      this.OnTextChanged = OnTextChanged;
      this.OnSuggestionChosen = OnSuggestionChosen;

      OnEnter = new ArglessRelayCommand(onEnter);
      OnDownKey = new ArglessRelayCommand(onDownKey);
      OnUpKey = new ArglessRelayCommand(onUpKey);
      OnEscape = new ArglessRelayCommand(() => { ShowPopup = false; });
    }

    // IMO this is still tight coupling with the View
    private ObservableCollection<object> suggestions = new();
    private bool isEnabled = true;
    private string searchQuery;
    private int selectedIndex;
    private bool showPopup;
    public ObservableCollection<object> Suggestions {
      get => suggestions;
      set => SetField(ref suggestions, value);
    }
    public bool IsEnabled {
      get => isEnabled;
      set => SetField(ref isEnabled, value);
    }
    public string SearchQuery {
      get => searchQuery;
      set {
        SetField(ref searchQuery, value);
        TextChanged();
      }
    }
    public int SelectedIndex {
      get => selectedIndex;
      set {
        SetField(ref selectedIndex, value);
        if (CallerId == "InvokeMethod") {
          SelectionChanged();
        }
      }
    }
    public bool ShowPopup {
      get => showPopup;
      set => SetField(ref showPopup, value);
    }

    private void TextChanged() {
      if (string.IsNullOrEmpty(SearchQuery)) {
        ShowPopup = false;
      } else {
        ShowPopup = true;
        OnTextChanged(Suggestions, SearchQuery);
      }
    }

    public ICommand OnEnter { get; }
    public ICommand OnDownKey { get; }
    public ICommand OnUpKey { get; }
    public ICommand OnEscape { get; }
    private void onEnter() {
      if (SelectedIndex >= 0) {
        DisplaySelection();
      } else if (!string.IsNullOrEmpty(SearchQuery)) {
        OnTextChanged(Suggestions, SearchQuery);
        if (Suggestions.Count > 0) {
          // triggers SelectionChanged, so no need to call DisplaySelection()
          SelectedIndex = 0;
        }
      }
    }
    private void onDownKey() {
      var count = Suggestions.Count;
      if (count > 0 && SelectedIndex < (count - 1)) {
        ShowPopup = true;
        SelectedIndex++;
      }
    }
    private void onUpKey() {
      if (Suggestions.Count > 0 && SelectedIndex > -1) {
        SelectedIndex--;
      }
    }

    private void SelectionChanged() {
      if (SelectedIndex >= 0) {
        DisplaySelection();
      }
    }

    private void DisplaySelection() {
      ShowPopup = false;
      OnSuggestionChosen(Selection, Suggestions[SelectedIndex]);
      SelectedIndex = -1;

      IsEnabled = false;
      SearchQuery = "";
    }
  }
}
