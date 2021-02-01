using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HunterPie.Core;
using HunterPie.GUI;
using MHWItemBoxTracker.Config;

namespace MHWItemBoxTracker.Controller
{
    class ItemBoxTracker
    {
        private Player player { get; }
        private GUI.ItemBoxTracker gui;

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
            Dispatch(() => gui.setItemsToDisplay(itemBoxRows));
        }

        public void unloadItemBox(object source = null, EventArgs e = null)
        {
            Dispatch(() => gui.setItemsToDisplay(new List<GUI.ItemBoxRow>()));
        }

        public void unregister() {
            Dispatch(() => Overlay.UnregisterWidget(gui));
        }

        private void Dispatch(System.Action function) =>
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, function);
    }
}
