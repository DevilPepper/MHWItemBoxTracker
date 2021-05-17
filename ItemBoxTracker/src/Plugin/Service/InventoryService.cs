using System.Linq;
using System.Threading.Tasks;
using MHWItemBoxTracker.Model;

namespace MHWItemBoxTracker.Service {
  public class InventoryService {
    private HunterPieService HP;
    private ConfigService Config;
    private SettingsModel Settings;
    private InventoryModel Data;

    public InventoryService(HunterPieService HP, ConfigService Config) {
      this.HP = HP;
      this.Config = Config;
    }

    public async Task<InventoryModel> LoadAsync() {
      if (Data == null) {
        Settings = await Config.LoadAsync();
        Data = new();
      }
      return Data;
    }

    public void Refresh() {
      Data.InVillage = HP.InHarvestZone;
      var always = Settings.Always.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInVillage = true,
        TrackInQuest = true,
        TrackCraftable = Settings.Always.TrackCraftable,
        TrackBox = Settings.Always.TrackBox,
        TrackPouch = Settings.Always.TrackPouch,
      });
      var village = Settings.Village.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInVillage = true,
        TrackCraftable = Settings.Village.TrackCraftable,
        TrackBox = Settings.Village.TrackBox,
        TrackPouch = Settings.Village.TrackPouch,
      });
      var quest = Settings.Quest.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInQuest = true,
        TrackCraftable = Settings.Quest.TrackCraftable,
        TrackBox = Settings.Quest.TrackBox,
        TrackPouch = Settings.Quest.TrackPouch,
      });

      var itemsInSettings = always.Union(village).Union(quest);

      var remove = Data.Items.Except(itemsInSettings);
      var add = itemsInSettings.Except(Data.Items);

      foreach (var item in remove) {
        Data.Items.Remove(item);
      }
      foreach (var item in add) {
        Data.Items.Add(item);
      }

      var ids = Data.Items.Select(i => i.Item.ItemId).ToHashSet();
      var box = HP.FindItemsInBox(ids);
      var pouch = HP.FindItemsInPouch(ids);
      var recipeList = HP.FindRecipes(ids);

      foreach (var item in Data.Items) {
        box.TryGetValue(item.Item.ItemId, out var AmountInBox);
        pouch.TryGetValue(item.Item.ItemId, out var AmountInPouch);
        recipeList.TryGetValue(item.Item.ItemId, out var recipes);

        var AmountCraftable = (item.TrackPouch ? HP.FindCraftableInPouch(recipes) : 0)
          + (item.TrackBox ? HP.FindCraftableInBox(recipes) : 0);

        item.AmountInBox = item.TrackBox ? AmountInBox : 0;
        item.AmountInPouch = item.TrackPouch ? AmountInPouch : 0;
        item.AmountCraftable = item.TrackCraftable ? AmountCraftable : 0;
      }
    }
  }
}
