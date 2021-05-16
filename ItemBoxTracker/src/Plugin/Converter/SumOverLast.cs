using System.Linq;

namespace MHWItemBoxTracker.Converter {
  public class SumOverLast : GenericMultiValueConverter<double> {
    public override double Convert(object[] values) {
      var size = values.Length;
      var sum = values.Take(size - 1).Sum(x => (int)x);
      var last = (int)values[size - 1];

      return sum * 1f / last;
    }
  }
}
