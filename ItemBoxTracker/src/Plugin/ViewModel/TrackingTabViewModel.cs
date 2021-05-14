using System.Collections.ObjectModel;
using System.Windows.Input;
using HunterPie.UI.Infrastructure;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.ViewModel {
  public class TrackingTabViewModel : NotifyPropertyChanged {
    public TrackingTabModel Model { get; }
    public ICommand AddRow { get; }
    public ICommand DeleteRow { get; }
    public ICommand MoveUp { get; }
    public ICommand MoveDown { get; }
    private ObservableCollection<ItemModel> items;

    public TrackingTabViewModel(TrackingTabModel Model) {
      this.Model = Model;
      if (Model.Tracking.Count == 0) {
        Model.Tracking.Add(new());
      }

      AddRow = new ArglessRelayCommand(() => Model.Tracking.Add(new()));
      DeleteRow = new RelayCommand(item => deleteRow(item as ItemModel));
      MoveUp = new RelayCommand(item => moveUp(item as ItemModel));
      MoveDown = new RelayCommand(item => moveDown(item as ItemModel));
    }

    public ObservableCollection<ItemModel> Items {
      get => items;
      set => SetField(ref items, value);
    }

    private void deleteRow(ItemModel item) {
      Model.Tracking.Remove(item);
      if (Model.Tracking.Count == 0) {
        Model.Tracking.Add(new());
      }
    }
    private void moveUp(ItemModel item) {
      var index = Model.Tracking.IndexOf(item);
      if (index > 0) {
        Model.Tracking.Move(index, index - 1);
      }
    }

    private void moveDown(ItemModel item) {
      var index = Model.Tracking.IndexOf(item);
      if (index < Model.Tracking.Count - 1) {
        Model.Tracking.Move(index, index + 1);
      }
    }
  }
}
