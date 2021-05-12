
using System.Collections.ObjectModel;
using System.Globalization;
using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.ViewModel {
  public class ItemViewModel : SearchableViewModel {
    private ObservableCollection<ItemConfig> currentlySelected;
    public ObservableCollection<ItemConfig> CurrentlySelected {
      get => currentlySelected;
      set {
        SetField(ref currentlySelected, value);
      }
    }
    public ItemViewModel(ItemConfig Data) : base(Data) { }

    public override void SuggestionChosen() {
      var data = Data as ItemConfig;
      var selectedItem = SuggestionsView.CurrentItem as ItemConfig;
      data.ItemId = selectedItem.ItemId;
      data.Name = selectedItem.Name;
    }

    public override bool SuggestionsFilter(object Candidate) {
      var candidate = Candidate as ItemConfig;
      return CultureInfo.CurrentCulture.CompareInfo.IndexOf(candidate.Name, SearchQuery, CompareOptions.IgnoreCase) >= 0
          && !CurrentlySelected.Contains(candidate);
    }

    public override void ClearSelection() {
      var data = Data as ItemConfig;
      data.ItemId = 0;
    }
  }
}
