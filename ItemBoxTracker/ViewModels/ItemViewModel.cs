using System.ComponentModel.DataAnnotations;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModels
{
  public class ItemViewModel : NotifyPropertyChanged
  {
    private string name;
    private int itemId;
    private int amount;

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

    public ItemConfig ToConfig() {
      var config = new ItemConfig();
      config.Name = name;
      config.ItemId = itemId;
      config.Amount = amount;
      return config;
    }
  }
}
