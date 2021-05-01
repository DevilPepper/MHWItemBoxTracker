using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HunterPie.Core;
using HunterPie.GUI;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;
using HunterPie.Plugins;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker.Controller
{
    class ItemBoxTracker
    {
        private Player player { get; }
        private GUI.ItemBoxTracker gui;
        private static SemaphoreSlim locke = new SemaphoreSlim(1,1);

        public ItemBoxTracker(Player player)
        {
            this.player = player;
            Dispatch(() =>
            {
                gui = new GUI.ItemBoxTracker();
                Overlay.RegisterWidget(gui);
            });
        }

        public void loadItemBox(object source = null, EventArgs e = null)
        {
            if (!player.InHarvestZone) return;
            Dispatch(async () => {
                var config = (await loadSettings()).Always;

                var items = config.Tracking;
                var box = player.ItemBox;
                var ids = items.Select(ic => ic.ItemId).ToHashSet();

                var itemsHeld = box.FindItemsInBox(ids);
                var itemBoxRows = new List<GUI.ItemBoxRow>();
                foreach (ItemConfig item in items)
                {
                    int amountHeld = 0;
                    itemsHeld.TryGetValue(item.ItemId, out amountHeld);

                    itemBoxRows.Add(new GUI.ItemBoxRow
                    {
                        name = item.Name,
                        ratio = $"{amountHeld}/{item.Amount}",
                        progress = 100.0 * amountHeld / item.Amount,
                    });
                }
                gui.setItemsToDisplay(itemBoxRows);
            });
        }

        public void unloadItemBox(object source = null, EventArgs e = null)
        {
            Dispatch(() => gui.setItemsToDisplay(new List<GUI.ItemBoxRow>()));
        }

        public void unregister() {
            Dispatch(() => Overlay.UnregisterWidget(gui));
        }

        private async Task<ItemBoxTrackerConfig> loadSettings() {
          await locke.WaitAsync();
          try {
            // TODO: use Settings.xaml.cs
            var config = await Plugin.LoadJson<ItemBoxTrackerConfig>("settings.json");

            // Not writing a comparer just for this...
            var oldConfig = await Plugin.LoadJson<DeprecatedItemBoxTrackerConfig>("settings.json");
            if (oldConfig.Tracking.Count > 0) {
              config.Always.Tracking = oldConfig.Tracking.ToList();
              await Plugin.SaveJson<ItemBoxTrackerConfig>("settings.json", config);
            }

            return config;
          } finally {
            locke.Release();
          }
        }
    }
}
