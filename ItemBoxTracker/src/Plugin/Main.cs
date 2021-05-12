using System.Collections.Generic;
using HunterPie.Core;
using HunterPie.Plugins;
using HunterPie.Settings;
using MHWItemBoxTracker.Service;
using MHWItemBoxTracker.Views;

namespace MHWItemBoxTracker {
  public class Main : IPlugin, ISettingsOwner {
    public string Name { get; set; } = "ItemBoxTracker";
    public string Description { get; set; } = "A HunterPie plugin to track items the player is farming";
    public Game Context { get; set; }

    private ConfigService Config = new();
    private Controller.ItemBoxTracker tracker { get; set; }

    // Really bad singleton, but I think it's fine considering
    // the plugin gets instantiated by HunterPie
    // and everywhere that uses the singleton is part of this plugin
    // i.e. plugin is guarenteed to be instantiated
    private static Main instance = null;
    public static Main Plugin => instance;

    public Main() {
      instance = this;
    }

    public void Initialize(Game context) {
      Context = context;
      tracker = new Controller.ItemBoxTracker(context.Player, Config);

      var player = Context.Player;
      player.OnVillageEnter += tracker.loadItemBox;
      player.OnVillageLeave += tracker.unloadItemBox;
      player.ItemBox.OnItemBoxUpdate += tracker.loadItemBox;
    }

    public void Unload() {
      var player = Context.Player;
      player.OnVillageEnter -= tracker.loadItemBox;
      player.OnVillageLeave -= tracker.unloadItemBox;
      player.ItemBox.OnItemBoxUpdate -= tracker.loadItemBox;

      tracker.unloadItemBox();
      tracker.unregister();
    }

    public IEnumerable<ISettingsTab> GetSettings(ISettingsBuilder builder) {
      builder.AddTab(new SettingsView(Config));
      return builder.Value();
    }
  }
}
