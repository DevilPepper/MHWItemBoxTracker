using System.Collections.ObjectModel;
using System.Windows.Data;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.ViewModel;

namespace MHWItemBoxTracker.Converter {
  public class ItemToViewModel : GenericMultiValueConverter<ItemViewModel> {
    public override ItemViewModel Convert(object[] values) {
      var item = values[0] as ItemModel;
      var suggestions = values[1] as ObservableCollection<ItemModel>;
      var currentlySelected = values[2] as ObservableCollection<ItemModel>;

      return new ItemViewModel(item) {
        SuggestionsView = CollectionViewSource.GetDefaultView(suggestions),
        CurrentlySelected = currentlySelected
      };
    }
  }
}
