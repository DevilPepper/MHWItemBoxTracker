using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MHWItemBoxTracker.Converter {
  public abstract class GenericMultiValueConverter<T> : MarkupExtension, IMultiValueConverter {
    public abstract T Convert(object[] values);
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
      return Convert(values);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider) {
      return this;
    }
  }
}
