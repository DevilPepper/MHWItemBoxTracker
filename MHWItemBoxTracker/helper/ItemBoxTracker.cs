using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HunterPie.Core;
using HunterPie.GUI;
using MHWItemBoxTracker.Config;

namespace MHWItemBoxTracker.helper
{
    class ItemBoxTracker
    {
        private Player player { get; }
        private GUI.ItemBoxTracker gui;

        public ItemBoxTracker(Player player)
        {
            this.player = player;
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
              new Action(() =>
            {
                gui = new GUI.ItemBoxTracker();
                Overlay.RegisterWidget(gui);
            }));
        }

        public void loadItemBox(object source = null, EventArgs e = null)
        {
            if (!player.InHarvestZone) return;

            var items = ConfigLoader.loadConfig().Tracking;
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
            gui?.setItemsToDisplay(itemBoxRows);
        }

        public void unloadItemBox(object source = null, EventArgs e = null)
        {
            gui?.setItemsToDisplay(new List<GUI.ItemBoxRow>());
        }
    }
}
