namespace MHWItemBoxTracker.Converter {
  public class RatioToPercent : GenericMultiValueConverter<double> {
    public override double Convert(object[] values) {
      var num = (int)values[0];
      var den = (int)values[1];

      return num * 100f / den;
    }
  }
}
