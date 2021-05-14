
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HunterPie.Core;
using MHWItemBoxTracker.Model;
using MHWItemBoxTracker.Utils;

namespace MHWItemBoxTracker.Service {
  public class InventoryService {
    // TODO: Probably need this
    // private static readonly SemaphoreSlim locke = new(1, 1);
    private Game Context;
    private ConfigService Config;
    private SettingsModel Settings;
    private InventoryModel Data;

    public InventoryService(Game Context, ConfigService Config) {
      this.Context = Context;
      this.Config = Config;
    }

    public async Task<InventoryModel> LoadAsync() {
      if (Data == null) {
        Settings = await Config.LoadAsync();
        Data = new() {
          InVillage = Context.Player.InHarvestZone,
        };
      }
      Refresh();
      return Data;
    }

    public void Refresh() {
      var always = Settings.Always.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInVillage = true,
        TrackInQuest = true,
        TrackCraftable = Settings.Always.TrackCraftable,
      });
      var village = Settings.Village.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInVillage = true,
        TrackCraftable = Settings.Village.TrackCraftable,
      });
      var quest = Settings.Quest.Tracking.Select(i => new InventoryItemModel() {
        Item = i,
        TrackInQuest = true,
        TrackCraftable = Settings.Quest.TrackCraftable,
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
      var box = Context.Player.ItemBox?.FindItemsInBox(ids) ?? new();
      foreach (var item in Data.Items) {
        box.TryGetValue(item.Item.ItemId, out int amountInBox);
        item.AmountInBox = amountInBox;
      }
    }

    // TODO: Probably move these to an event handler class or something
    public void Subscribe() {
      var player = Context.Player;
      player.OnVillageEnter += EnterVillage;
      player.OnVillageLeave += LeaveVillage;
      player.ItemBox.OnItemBoxUpdate += Refresh;
      // TODO: subscribe to save event?
    }

    public void Unsubscribe() {
      var player = Context.Player;
      player.OnVillageEnter -= EnterVillage;
      player.OnVillageLeave -= LeaveVillage;
      player.ItemBox.OnItemBoxUpdate -= Refresh;
    }

    public void Refresh(object source, EventArgs e) {
      Dispatcher.Dispatch(Refresh);
    }

    private void EnterVillage(object source, EventArgs e) {
      Dispatcher.Dispatch(() => {
        Data.InVillage = true;
        Refresh();
      });
    }

    private void LeaveVillage(object source, EventArgs e) {
      Dispatcher.Dispatch(() => {
        Data.InVillage = false;
        Refresh();
      });
    }
  }
}
