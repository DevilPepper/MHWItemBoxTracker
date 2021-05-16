using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Model {
  public class AmountsModel : NotifyPropertyChanged {
    private int inBox = 0;
    private int inPouch = 0;
    private int craftable = 0;
    private int wanted = 0;

    public int InBox {
      get => inBox;
      set => SetField(ref inBox, value);
    }
    public int InPouch {
      get => inPouch;
      set => SetField(ref inPouch, value);
    }
    public int Craftable {
      get => craftable;
      set => SetField(ref craftable, value);
    }
    public int Wanted {
      get => wanted;
      set => SetField(ref wanted, value);
    }
  }
}
