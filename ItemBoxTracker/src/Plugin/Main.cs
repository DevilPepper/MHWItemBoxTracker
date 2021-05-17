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
    private EventService Events;
    private HunterPieService HP;
    private InventoryService Inventory;
    private InventoryView GUI;

    private SettingsView settings;
    private SettingsView Settings {
      get {
        lock (Config) {
          if (settings == null) {
            settings = new(Config);
          }
        }
        return settings;
      }
    }

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
      HP = new(Context);
      Inventory = new(HP, Config);

      Dispatcher.Dispatch(async () => {
        GUI = new(await Inventory.LoadAsync());
        Events = new(Context, GUI, Inventory, Settings);
        Events.Subscribe();
        Overlay.RegisterWidget(GUI);
        await GUI.Initialize();
      });
    }

    public void Unload() {
      Dispatcher.Dispatch(() => {
        Events.Unsubscribe();
        GUI.Unload();
        Overlay.UnregisterWidget(GUI);
      });
    }

    public IEnumerable<ISettingsTab> GetSettings(ISettingsBuilder builder) {
      builder.AddTab(Settings);
      return builder.Value();
    }
  }
}
