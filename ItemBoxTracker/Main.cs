using System.Collections.Generic;

using HunterPie.Core;
using HunterPie.Plugins;
using HunterPie.Settings;
using static MHWItemBoxTracker.Utils.Dispatcher;

namespace MHWItemBoxTracker {
  public class Main : IPlugin, ISettingsOwner {
    public string Name { get; set; }
    public string Description { get; set; }
    public Game Context { get; set; }
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
      Dispatch(async () => {
        var module = await this.LoadJson<PluginInformation>("module.json");
        Name = module.Name;
        Description = module.Description;
      });
      Context = context;

      tracker = new Controller.ItemBoxTracker(context.Player);
      hookEvents();
    }

    public void Unload() {
      unhookEvents();
      tracker.unregister();
    }

    internal void hookEvents() {
      var player = Context.Player;
      player.OnVillageEnter += tracker.loadItemBox;
      player.OnVillageLeave += tracker.unloadItemBox;
      player.ItemBox.OnItemBoxUpdate += tracker.loadItemBox;
    }

    internal void unhookEvents() {
      var player = Context.Player;
      player.OnVillageEnter -= tracker.loadItemBox;
      player.OnVillageLeave -= tracker.unloadItemBox;
      player.ItemBox.OnItemBoxUpdate -= tracker.loadItemBox;

      tracker.unloadItemBox();
    }

    public IEnumerable<ISettingsTab> GetSettings(ISettingsBuilder builder) {
      builder.AddTab(new GUI.Settings());
      return builder.Value();
    }
  }
}
