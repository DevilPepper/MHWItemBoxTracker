using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MHWItemBoxTracker.Utils {

  public class NotifyPropertyChanged : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
      if (EqualityComparer<T>.Default.Equals(field, value)) return false;
      field = value;
      OnPropertyChanged(propertyName);
      return true;
    }

    protected string CallerId { get => new StackTrace().GetFrames()[2].GetMethod().Name; }
  }
}
