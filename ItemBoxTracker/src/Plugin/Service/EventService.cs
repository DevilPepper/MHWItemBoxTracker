using System;
using HunterPie.Core;
using MHWItemBoxTracker.Utils;
using MHWItemBoxTracker.Views;

namespace MHWItemBoxTracker.Service {
  public class EventService {
    private Game Context;
    InventoryView GUI;
    InventoryService Inventory;
    public EventService(Game Context, InventoryView GUI, InventoryService Inventory) {
      this.Context = Context;
      this.GUI = GUI;
      this.Inventory = Inventory;
    }

    public void Subscribe() {
      var player = Context.Player;
      player.OnVillageEnter += Refresh;
      player.OnVillageLeave += Refresh;
      player.ItemBox.OnItemBoxUpdate += Refresh;
      player.Inventory.OnInventoryUpdate += Refresh;
      player.OnCharacterLogin += ShowWidget;
      player.OnCharacterLogout += HideWidget;
      // TODO: subscribe to save event?
    }

    public void Unsubscribe() {
      var player = Context.Player;
      player.OnVillageEnter -= Refresh;
      player.OnVillageLeave -= Refresh;
      player.ItemBox.OnItemBoxUpdate -= Refresh;
      player.Inventory.OnInventoryUpdate -= Refresh;
      player.OnCharacterLogin -= ShowWidget;
      player.OnCharacterLogout -= HideWidget;
    }

    public void Refresh(object source, EventArgs e) {
      Dispatcher.Dispatch(Inventory.Refresh);
    }
    public void ShowWidget(object source, EventArgs e) {
      Dispatcher.Dispatch(() => GUI.ShouldShow = true);
    }
    public void HideWidget(object source, EventArgs e) {
      Dispatcher.Dispatch(() => GUI.ShouldShow = false);
    }
  }
}
