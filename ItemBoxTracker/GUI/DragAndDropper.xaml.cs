using System.Windows;
using System.Windows.Controls;

namespace MHWItemBoxTracker.GUI {
  // TODO: revisit this some day
  public partial class DragAndDropper : UserControl {

    public ListView Child {
      get { return (ListView)GetValue(ChildProperty); }
      set { SetValue(ChildProperty, value); }
    }
    public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
    "Child", typeof(ListView), typeof(DragAndDropper), new PropertyMetadata(null));
    public DragAndDropper() : base() {
      InitializeComponent();
    }
  }
}
