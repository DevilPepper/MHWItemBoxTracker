using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels {
  public class ItemViewModel : NotifyPropertyChanged {
    private string name;
    private int itemId;
    private int amount;

    public ItemViewModel() { }

    public ItemViewModel(ItemConfig config) {
      name = config.Name;
      itemId = config.ItemId;
      amount = config.Amount;
    }

    public string Name {
      get => name;
      set => SetField(ref name, value);
    }

    public int ItemId {
      get => itemId;
      set => SetField(ref itemId, value);
    }

    public int Amount {
      get => amount;
      set => SetField(ref amount, value);
    }

    public override bool Equals(object obj) {
      if ((obj != null) && GetType().Equals(obj.GetType())) {
        var Obj = (ItemViewModel)obj;
        return Obj.ItemId == ItemId;
      }
      return false;
    }

    public override int GetHashCode() {
      return ItemId;
    }

    public ItemConfig ToConfig() {
      return new ItemConfig {
        Name = name,
        ItemId = itemId,
        Amount = amount
      };
    }
  }
}
