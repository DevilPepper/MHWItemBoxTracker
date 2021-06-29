using System.Threading.Tasks;
using HunterPie.Core.Settings;
using HunterPie.GUI;
using HunterPie.Plugins;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.ViewModel;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Views {
  public partial class InventoryWidget : Widget {
    private static readonly string settingsJson = "widget.settings.json";
    private ItemBoxWidgetSettings widgetSettings { get; set; }
    public override IWidgetSettings Settings => widgetSettings;
    public InventoryViewModel VM { get; }
    public bool ShouldShow {
      set {
        WidgetHasContent = value;
        ChangeVisibility();
      }
    }

    public InventoryWidget(InventoryModel Inventory) : base() {
      InitializeComponent();
      SetWindowFlags();
      VM = new(Inventory);
      DataContext = this;
    }

    public async Task Initialize() {
      widgetSettings = await Plugin.LoadJson<ItemBoxWidgetSettings>(settingsJson);
      ApplySettings();
    }

    public void Unload() {
      ShouldShow = false;
    }

    public override void EnterWidgetDesignMode() {
      base.EnterWidgetDesignMode();
      RemoveWindowTransparencyFlag();
    }

    public async override void LeaveWidgetDesignMode() {
      ApplyWindowTransparencyFlag();
      base.LeaveWidgetDesignMode();
      await Plugin.SaveJson(settingsJson, widgetSettings);
    }

    public override void ScaleWidget(double newScaleX, double newScaleY) {
      base.ScaleWidget(newScaleX, newScaleY);
      contents.LayoutTransform = LayoutTransform;
    }
  }
}
