using System;
using System.ComponentModel;
using System.Windows.Input;
using HunterPie.UI.Infrastructure;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModel {
  public abstract class SearchableViewModel : NotifyPropertyChanged {
    public virtual object Data { get; }
    private ICollectionView suggestionsView;
    private string searchQuery;
    private bool showPopup;
    private bool isEnabled = true;

    public abstract bool SuggestionsFilter(object candidate);
    public abstract void SuggestionChosen();
    public abstract void ClearSelection();
    public SearchableViewModel(object Data) {
      this.Data = Data;

      OnEnter = new ArglessRelayCommand(onEnter);
      OnDownKey = new ArglessRelayCommand(onDownKey);
      OnUpKey = new ArglessRelayCommand(onUpKey);
      OnEscape = new ArglessRelayCommand(() => { ShowPopup = false; });
      OnClear = new ArglessRelayCommand(() => {
        IsEnabled = true;
        ClearSelection();
      });
      OnListSelected = new ArglessRelayCommand(DisplaySelection);
      OnLoaded = new ArglessRelayCommand(onLoaded);
    }
    public virtual bool IsEnabled {
      get => isEnabled;
      set => SetField(ref isEnabled, value);
    }
    public virtual string SearchQuery {
      get => searchQuery;
      set {
        SetField(ref searchQuery, value);
        ShowPopup = !string.IsNullOrEmpty(SearchQuery);
        SuggestionsView.Refresh();
        SuggestionsView.MoveCurrentToPosition(-1);
      }
    }

    public virtual ICollectionView SuggestionsView {
      get => suggestionsView;
      set {
        SetField(ref suggestionsView, value);
        SuggestionsView.Filter =
          candidate => string.IsNullOrEmpty(SearchQuery) || SuggestionsFilter(candidate);
      }
    }
    public virtual bool ShowPopup {
      get => showPopup;
      set => SetField(ref showPopup, value);
    }

    public ICommand OnEnter { get; }
    public ICommand OnDownKey { get; }
    public ICommand OnUpKey { get; }
    public ICommand OnEscape { get; }
    public ICommand OnLoaded { get; }
    public ICommand OnClear { get; }
    public ICommand OnListSelected { get; }
    private void onEnter() {
      if (SuggestionsView.CurrentPosition >= 0) {
        DisplaySelection();
      } else if (!string.IsNullOrEmpty(SearchQuery)) {
        SuggestionsView.Refresh();
        if (SuggestionsView.MoveCurrentToFirst()) {
          DisplaySelection();
        }
      }
    }
    private void onDownKey() {
      if (!SuggestionsView.IsEmpty) {
        ShowPopup = true;
        SuggestionsView.MoveCurrentToNext();
      }
    }
    private void onUpKey() {
      SuggestionsView.MoveCurrentToPrevious();
    }

    private void onLoaded() {
      var empty = Activator.CreateInstance(Data.GetType());
      if (!empty.Equals(Data)) {
        IsEnabled = false;
      }
      // else {
      //   searchInput.Focus();
      // }
    }

    private void DisplaySelection() {
      if (SuggestionsView.CurrentPosition >= 0) {
        ShowPopup = false;
        SuggestionChosen();
        IsEnabled = false;
        SearchQuery = "";
      }
    }
  }
}
