using System.Collections.Generic;
using System.Threading.Tasks;
using HunterPie.Core.Settings;
using HunterPie.GUI;
using HunterPie.Plugins;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Service;
using MHWItemBoxTracker.ViewModel;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.Views {
  public partial class InventoryView : Widget {
    private static readonly string settingsJson = "widget.settings.json";
    private ItemBoxWidgetSettings widgetSettings { get; set; }
    public override IWidgetSettings Settings => widgetSettings;
    InventoryService Inventory;
    private InventoryViewModel VM;
    public InventoryView(InventoryService Inventory) : base() {
      InitializeComponent();
      this.Inventory = Inventory;
      VM = new(new());
      DataContext = VM;
    }

    public async Task Initialize() {
      widgetSettings = await Plugin.LoadJson<ItemBoxWidgetSettings>(settingsJson);
      ApplySettings();

      var data = await Inventory.LoadAsync();
      VM = new(data);
      DataContext = VM;

      WidgetHasContent = true;
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
