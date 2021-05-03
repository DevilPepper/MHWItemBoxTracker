using System.Collections.Generic;
using HunterPie.Core.Settings;
using HunterPie.GUI;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.GUI {
  public partial class ItemBoxTracker : Widget {
    private static readonly string settingsJson = "widget.settings.json";
    private ItemBoxWidgetSettings widgetSettings { get; set; }
    public override IWidgetSettings Settings => widgetSettings;
    public ItemBoxTracker() : base() {
      InitializeComponent();
      Dispatch(async () => {
        widgetSettings = await Plugin.LoadJson<ItemBoxWidgetSettings>(settingsJson);
        ApplySettings();
      });
    }

    public void setItemsToDisplay(List<ItemBoxRow> itemBoxRows) {
      // Debugger.Log($"Theme: {UserSettings.PlayerConfig.HunterPie.Theme}");
      theList.ItemsSource = itemBoxRows;
      WidgetHasContent = (itemBoxRows.Count > 0);
      ChangeVisibility();
    }

    public override void EnterWidgetDesignMode() {
      base.EnterWidgetDesignMode();
      RemoveWindowTransparencyFlag();
    }

    public override void LeaveWidgetDesignMode() {
      base.LeaveWidgetDesignMode();
      ApplyWindowTransparencyFlag();
      Dispatch(async () => { await Plugin.SaveJson(settingsJson, widgetSettings); });
    }
  }

  public class ItemBoxRow {
    public string name { get; set; }
    public string ratio { get; set; }
    public double progress { get; set; }
  }
}
