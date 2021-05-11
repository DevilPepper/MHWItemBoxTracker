using System.ComponentModel.DataAnnotations;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class ItemConfig : NotifyPropertyChanged {
    private string name;
    public int itemId;
    public int amount;

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
  }
}
