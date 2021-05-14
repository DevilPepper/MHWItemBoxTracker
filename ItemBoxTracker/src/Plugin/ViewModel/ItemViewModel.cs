
using System.Collections.ObjectModel;
using System.Globalization;
using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.ViewModel {
  public class ItemViewModel : SearchableViewModel {
    private ObservableCollection<ItemModel> currentlySelected;
    public ObservableCollection<ItemModel> CurrentlySelected {
      get => currentlySelected;
      set {
        SetField(ref currentlySelected, value);
      }
    }
    public ItemViewModel(ItemModel Data) : base(Data) { }

    public override void SuggestionChosen() {
      var data = Data as ItemModel;
      var selectedItem = SuggestionsView.CurrentItem as ItemModel;
      data.ItemId = selectedItem.ItemId;
      data.Name = selectedItem.Name;
    }

    public override bool SuggestionsFilter(object Candidate) {
      var candidate = Candidate as ItemModel;
      return CultureInfo.CurrentCulture.CompareInfo.IndexOf(candidate.Name, SearchQuery, CompareOptions.IgnoreCase) >= 0
          && !CurrentlySelected.Contains(candidate);
    }

    public override void ClearSelection() {
      var data = Data as ItemModel;
      data.ItemId = 0;
    }
  }
}
