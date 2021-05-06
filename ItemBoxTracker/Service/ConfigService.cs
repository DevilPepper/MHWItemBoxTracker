
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HunterPie.Plugins;
using MHWItemBoxTracker.Config;
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
            config.Always.Tracking = oldConfig.Tracking.ToList();
            await Plugin.SaveJson(settings, config);
          }
          Config = config;
        }

        return Config;
      } finally {
        locke.Release();
      }
    }

    public async Task SaveAsync(ItemBoxTrackerConfig config) {
      await locke.WaitAsync();
      try {
        Config = config;
        await Plugin.SaveJson(settings, Config);
      } finally {
        locke.Release();
      }
    }
  }
}
