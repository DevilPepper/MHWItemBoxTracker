
using System.Collections.ObjectModel;
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
    }
    public bool InventoryFilter(InventoryItemModel _) {
      return true;
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
