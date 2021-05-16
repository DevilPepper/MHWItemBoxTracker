using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using HunterPie.Plugins;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Converter {
  public abstract class GenericValueConverter<TFrom, TTo> : MarkupExtension, IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if (value == null) return null;

      // Plugin.Log($"typeof({typeof(TTo)}) != {targetType}");
      // Plugin.Log($"!({value.GetType()}).IsAssignableFrom({typeof(TFrom)})");
      // if (typeof(TTo) != targetType) return null;
      if (!value.GetType().IsAssignableFrom(typeof(TFrom))) return null;
      return Convert((TFrom)value, parameter);
    }

    public abstract TTo Convert(TFrom value, object parameter);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }


    #region MarkupExtension members

    public override object ProvideValue(IServiceProvider serviceProvider) {
      return this;
    }

    #endregion
  }
}
