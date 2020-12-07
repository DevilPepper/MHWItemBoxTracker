using System;
using System.Linq;
using HunterPie.Core;
using MHWItemBoxTracker.Config;
using Debugger = HunterPie.Logger.Debugger;

namespace MHWItemBoxTracker.helper
{
    class ItemBoxTracker
    {
        private Player player { get; }

        public ItemBoxTracker(Player player)
        {
            this.player = player;
        }

        public void loadItemBox(object source = null, EventArgs e = null)
        {
            if (!player.InHarvestZone) return;

            Debugger.Log("Loading Box!!!");

            var items = ConfigLoader.loadConfig().tracking;
            var box = player.ItemBox;
            var ids = items.Select(ic => ic.itemId).ToHashSet();

            Debugger.Log(String.Join(",", ids));
            var itemsHeld = box.FindItemsInBox(ids);
            Debugger.Log(String.Join(",", itemsHeld));
            foreach (ItemConfig item in items){
                int amountHeld = 0;
                itemsHeld.TryGetValue(item.itemId, out amountHeld);
                Debugger.Log($"{item.name}: {amountHeld}/{item.amount}");
            }
        }

        public void unloadItemBox(object source = null, EventArgs e = null)
        {
            Debugger.Log("Hiding Box!!!");
        }
    }
}
