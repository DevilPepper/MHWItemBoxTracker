using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.Converter {
  public class InventoryToAmounts : GenericValueConverter<InventoryItemModel, AmountsModel> {
    public override AmountsModel Convert(InventoryItemModel value, object parameter) {
      return new() {
        InPouch = value.AmountInPouch,
        InBox = value.AmountInBox,
        Craftable = value.AmountCraftable,
        Wanted = value.Item.Amount,
      };
    }
  }
}
