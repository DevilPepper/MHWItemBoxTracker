using HunterPie.UI.Infrastructure.Converters;
using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.Converter {
  public class InventoryToAmountsText : GenericMultiValueConverter<string> {
    public override string Convert(object[] values) {
      var AmountInBox = (int)values[0];
      var AmountInPouch = (int)values[1];
      var AmountCraftable = (int)values[2];
      var AmountWanted = (int)values[3];

      var TrackPouch = (bool)values[4];
      var TrackBox = (bool)values[5];
      var TrackCraftable = (bool)values[6];

      var pouch = TrackPouch ? $"{AmountInPouch}" : "";
      var box = TrackBox ? $"{AmountInBox}" : "";
      var craftable = TrackCraftable ? $" (+{AmountCraftable})" : "";
      var divider = (TrackPouch && TrackBox) ? " | " : "";
      var wanted = (TrackPouch || TrackBox || TrackCraftable) ? $" / {AmountWanted}" : "";

      return $"{pouch}{divider}{box}{craftable}{wanted}";
    }
  }
}
