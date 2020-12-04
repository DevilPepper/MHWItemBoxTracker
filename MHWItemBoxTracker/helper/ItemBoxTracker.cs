using System;
using HunterPie.Core;
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

        internal void hookEvents()
        {
            player.OnVillageEnter += loadItemBox;
            player.OnVillageLeave += unloadItemBox;
        }

        internal void unhookEvents()
        {
            player.OnVillageEnter -= loadItemBox;
            player.OnVillageLeave -= unloadItemBox;
        }

        public void loadItemBox(object source, EventArgs e)
        {
            Debugger.Debug("Loading Box!!!");
        }

        public void unloadItemBox(object source, EventArgs e)
        {
            Debugger.Debug("Hiding Box!!!");
        }
    }
}
