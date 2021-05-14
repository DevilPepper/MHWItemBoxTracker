
using System.ComponentModel;
using System.Windows.Data;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModel {
  public class InventoryViewModel : NotifyPropertyChanged {
    public InventoryModel Data { get; }
    private ICollectionView items;

    public InventoryViewModel(InventoryModel Data) {
      this.Data = Data;
      Items = CollectionViewSource.GetDefaultView(Data.Items);
      Data.PropertyChanged += (sender, e) => {
        if (e.PropertyName == nameof(Data.InVillage)) {
          Items?.Refresh();
        }
      };
    }
    public bool InventoryFilter(InventoryItemModel candidate) {
      return (candidate.TrackInVillage && Data.InVillage)
      || (candidate.TrackInQuest && !Data.InVillage);
    }

    public ICollectionView Items {
      get => items;
      set {
        SetField(ref items, value);
        Items.Filter = candidate => InventoryFilter(candidate as InventoryItemModel);
      }
    }
  }
}
