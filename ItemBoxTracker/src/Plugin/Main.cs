using System.Collections.Generic;
using HunterPie.Core;
using HunterPie.GUI;
using HunterPie.Plugins;
using HunterPie.Settings;
using MHWItemBoxTracker.Service;
using MHWItemBoxTracker.Utils;
using MHWItemBoxTracker.Views;

namespace MHWItemBoxTracker {
  public class Main : IPlugin, ISettingsOwner {
    public string Name { get; set; } = "ItemBoxTracker";
    public string Description { get; set; } = "A HunterPie plugin to track items the player is farming";
    public Game Context { get; set; }

    private readonly ConfigService Config = new();
    private InventoryService Inventory;
    private InventoryView GUI;

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
      Inventory = new(Context, Config);

      Dispatcher.Dispatch(async () => {
        GUI = new(Inventory);
        Overlay.RegisterWidget(GUI);
        await GUI.Initialize();
        Inventory.Subscribe();
      });
    }

    public void Unload() {
      Inventory.Unsubscribe();
      Dispatcher.Dispatch(() => {
        Overlay.UnregisterWidget(GUI);
      });
    }

    public IEnumerable<ISettingsTab> GetSettings(ISettingsBuilder builder) {
      builder.AddTab(new SettingsView(Config));
      return builder.Value();
    }
  }
}
