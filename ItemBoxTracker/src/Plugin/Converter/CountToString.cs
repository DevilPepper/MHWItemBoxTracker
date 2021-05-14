using HunterPie.UI.Infrastructure.Converters;

namespace MHWItemBoxTracker.Converter {
  public class CountToString : GenericValueConverter<int, string> {
    public string Zero { get; set; }
    public string More { get; set; }
    public override string Convert(int value, object _) {
      return value > 0 ? More : Zero;
    }
  }
}
