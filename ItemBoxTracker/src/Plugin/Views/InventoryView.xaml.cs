using System.Threading.Tasks;
using HunterPie.Core.Settings;
using HunterPie.GUI;
using HunterPie.Plugins;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.ViewModel;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Views {
  public partial class InventoryView : Widget {
    private static readonly string settingsJson = "widget.settings.json";
    private ItemBoxWidgetSettings widgetSettings { get; set; }
    public override IWidgetSettings Settings => widgetSettings;
    private InventoryViewModel VM;
    public InventoryView(InventoryModel Inventory) : base() {
      InitializeComponent();
      VM = new(Inventory);
      DataContext = VM;
    }

    public async Task Initialize() {
      widgetSettings = await Plugin.LoadJson<ItemBoxWidgetSettings>(settingsJson);
      ApplySettings();

      WidgetHasContent = true;
      ChangeVisibility();
    }

    public void Unload() {
      WidgetHasContent = false;
      ChangeVisibility();
    }

    public override void EnterWidgetDesignMode() {
      base.EnterWidgetDesignMode();
      RemoveWindowTransparencyFlag();
    }

    public async override void LeaveWidgetDesignMode() {
      base.LeaveWidgetDesignMode();
      ApplyWindowTransparencyFlag();
      await Plugin.SaveJson(settingsJson, widgetSettings);
    }
  }
}
