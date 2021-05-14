using HunterPie.Core.Native;
using HunterPie.UI.Infrastructure.Converters;

namespace MHWItemBoxTracker.Converter {
  public class ItemIdToDescription : GenericValueConverter<int, string> {
    public override string Convert(int value, object parameter) {
      return GMD.GetItemDescriptionById(value);
    }
  }
}
