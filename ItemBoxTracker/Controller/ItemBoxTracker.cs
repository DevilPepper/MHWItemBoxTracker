using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HunterPie.Core;
using HunterPie.GUI;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Service;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.Controller {
  class ItemBoxTracker {
    private Player player { get; }
    private GUI.ItemBoxTracker gui;

    private ConfigService Config;

    public ItemBoxTracker(Player player, ConfigService Config) {
      this.player = player;
      this.Config = Config;
      Dispatch(() => {
        gui = new GUI.ItemBoxTracker();
        Overlay.RegisterWidget(gui);
      });
    }

    public void loadItemBox(object source = null, EventArgs e = null) {
      if (!player.InHarvestZone) return;
      Dispatch(async () => {
        var config = (await Config.LoadAsync()).Always;

        var items = config.Tracking;
        var box = player.ItemBox;
        var ids = items.Select(ic => ic.ItemId).ToHashSet();

        var itemsHeld = box.FindItemsInBox(ids);
        var itemBoxRows = new List<GUI.ItemBoxRow>();
        foreach (ItemConfig item in items) {
          itemsHeld.TryGetValue(item.ItemId, out int amountHeld);

          itemBoxRows.Add(new GUI.ItemBoxRow {
            name = item.Name,
            ratio = $"{amountHeld}/{item.Amount}",
            progress = 100.0 * amountHeld / item.Amount,
          });
        }
        gui.setItemsToDisplay(itemBoxRows);
      });
    }

    public void unloadItemBox(object source = null, EventArgs e = null) {
      Dispatch(() => gui.setItemsToDisplay(new List<GUI.ItemBoxRow>()));
    }

    public void unregister() {
      Dispatch(() => Overlay.UnregisterWidget(gui));
    }


  }
}
