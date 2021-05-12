
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HunterPie.Plugins;
using MHWItemBoxTracker.Model;
using static MHWItemBoxTracker.Main;

namespace MHWItemBoxTracker.Service {
  public class ConfigService {
    public static string settings = "settings.json";
    private static readonly SemaphoreSlim locke = new(1, 1);
    private ItemBoxTrackerConfig Config;

    public async Task<ItemBoxTrackerConfig> LoadAsync() {
      await locke.WaitAsync();
      try {
        if (Config == null) {
          var config = await Plugin.LoadJson<ItemBoxTrackerConfig>(settings);
          // Not writing a comparer just for this...
          var oldConfig = await Plugin.LoadJson<DeprecatedItemBoxTrackerConfig>(settings);
          if (oldConfig.Tracking.Count > 0) {
            config.Always.Tracking = new ObservableCollection<ItemConfig>(oldConfig.Tracking);
            await Plugin.SaveJson(settings, config);
            Plugin.Log("Converted settings file to the new format");
          }
          Config = config;
        }

        return Config;
      } finally {
        locke.Release();
      }
    }

    public async Task SaveAsync() {
      await locke.WaitAsync();
      try {
        // TODO: Do this clean up better
        Config.Always.Tracking = new(Config.Always.Tracking.Where(i => i.ItemId > 0));
        Config.Village.Tracking = new(Config.Village.Tracking.Where(i => i.ItemId > 0));
        Config.Quest.Tracking = new(Config.Quest.Tracking.Where(i => i.ItemId > 0));

        await Plugin.SaveJson(settings, Config);
      } finally {
        locke.Release();
      }
    }
  }
}
