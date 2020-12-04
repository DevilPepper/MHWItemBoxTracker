using System;
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
            Debugger.Debug("Loading Box!!!");

            var items = ConfigLoader.loadConfig().tracking;
            //var box = player.Box;
        }

        public void unloadItemBox(object source, EventArgs e)
        {
            Debugger.Debug("Hiding Box!!!");
        }
    }
}
