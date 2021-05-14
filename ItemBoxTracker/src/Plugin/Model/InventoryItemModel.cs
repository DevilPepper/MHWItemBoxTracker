using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class InventoryItemModel : NotifyPropertyChanged {
    private ItemModel item = new();
    private int amountInBox = 0;
    private int amountInPouch = 0;
    private int amountCraftable = 0;
    public bool trackInVillage = false;
    public bool trackInQuest = false;
    public bool trackCraftable = false;

    public ItemModel Item {
      get => item;
      set => SetField(ref item, value);
    }

    public int AmountInBox {
      get => amountInBox;
      set => SetField(ref amountInBox, value);
    }
    public int AmountInPouch {
      get => amountInPouch;
      set => SetField(ref amountInPouch, value);
    }
    public int AmountCraftable {
      get => amountCraftable;
      set => SetField(ref amountCraftable, value);
    }
    public bool TrackInVillage {
      get => trackInVillage;
      set => SetField(ref trackInVillage, value);
    }
    public bool TrackInQuest {
      get => trackInQuest;
      set => SetField(ref trackInQuest, value);
    }
    public bool TrackCraftable {
      get => trackCraftable;
      set => SetField(ref trackCraftable, value);
    }

    public override bool Equals(object obj) {
      if ((obj != null) && GetType().Equals(obj.GetType())) {
        var Obj = (InventoryItemModel)obj;
        return Obj.Item.ItemId == Item.ItemId;
      }
      return false;
    }

    public override int GetHashCode() {
      return Item.ItemId;
    }
  }
}
