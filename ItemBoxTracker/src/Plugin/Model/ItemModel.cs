using System.ComponentModel.DataAnnotations;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class ItemModel : NotifyPropertyChanged {
    private string name;
    private int itemId;
    private int amount;

    public string Name {
      get => name;
      set => SetField(ref name, value);
    }

    [Required]
    public int ItemId {
      get => itemId;
      set => SetField(ref itemId, value);
    }

    [Required]
    public int Amount {
      get => amount;
      set => SetField(ref amount, value);
    }

    public override bool Equals(object obj) {
      if ((obj != null) && GetType().Equals(obj.GetType())) {
        var Obj = (ItemModel)obj;
        return Obj.ItemId == ItemId;
      }
      return false;
    }

    public override int GetHashCode() {
      return ItemId;
    }
  }
}
