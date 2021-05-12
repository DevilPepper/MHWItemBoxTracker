using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.ViewModel;

namespace MHWItemBoxTracker.Converter {
  public class ItemToViewModel : MarkupExtension, IMultiValueConverter {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
      var item = values[0] as ItemConfig;
      var suggestions = values[1] as ObservableCollection<ItemConfig>;
      var currentlySelected = values[2] as ObservableCollection<ItemConfig>;

      return new ItemViewModel(item) {
        SuggestionsView = CollectionViewSource.GetDefaultView(suggestions),
        CurrentlySelected = currentlySelected
      };
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider) {
      return this;
    }
  }
}
