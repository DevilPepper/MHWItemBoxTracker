using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.Converter {
  public class InventoryToAmountsText : GenericValueConverter<InventoryItemModel, string> {
    public override string Convert(InventoryItemModel value, object parameter) {
      var pouch = value.TrackPouch ? $"{value.AmountInPouch}" : "";
      var box = value.TrackBox ? $"{value.AmountInBox}" : "";
      var craftable = value.TrackCraftable ? $" (+{value.AmountCraftable})" : "";
      var divider = (value.TrackPouch && value.TrackBox) ? " | " : "";
      var wanted = (value.TrackPouch || value.TrackBox || value.TrackCraftable) ? $" / {value.Item.Amount}" : "";

      return $"{pouch}{divider}{box}{craftable}{wanted}";
    }
  }
}
