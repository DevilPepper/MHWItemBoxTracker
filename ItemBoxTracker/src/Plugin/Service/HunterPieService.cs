using System;
using System.Collections.Generic;
using System.Linq;
using HunterPie.Core;
using HunterPie.Core.Craft;
using HunterPie.Core.Definitions;

namespace MHWItemBoxTracker.Service {
  public class HunterPieService {
    private Game Context;
    public bool InHarvestZone {
      get => Context.Player.InHarvestZone;
    }
    public HunterPieService(Game Context) {
      this.Context = Context;
    }

    public Dictionary<int, int> FindItemsInBox(HashSet<int> ids) {
      return Context.Player.ItemBox?.FindItemsInBox(ids) ?? new();
    }

    public Dictionary<int, int> FindItemsInPouch(HashSet<int> ids) {
      return Context.Player.Inventory?.FindItemsAndAmmos(ids)?.ToDictionary(i => i.ItemId, i => i.Amount) ?? new();
    }

    public HashSet<int> GetMaterialsForRecipes(List<Recipe> recipes) {
      return recipes
        .SelectMany(r => r.MaterialsNeeded)
        .Select(m => m.ItemId)
        .ToHashSet();
    }

    public int FindCraftableInBox(List<Recipe> recipeList) {
      return FindCraftable(recipeList, ids => SearchBox(ids));
    }

    public int FindCraftableInPouch(List<Recipe> recipeList) {
      return FindCraftable(recipeList, ids => SearchPouch(ids));
    }
    public Dictionary<int, List<Recipe>> FindRecipes(HashSet<int> ids) {
      return ids.ToDictionary(id => id, id => Recipes.FindRecipes(id) ?? new());
    }

    private int FindCraftable(List<Recipe> recipeList, Func<HashSet<int>, sItem[]> FindItems) {
      var recipes = recipeList ?? new();
      var ids = GetMaterialsForRecipes(recipes);
      var materials = FindItems(ids) ?? new sItem[0];
      return recipes.Select(r => r.Calculate(materials)).Sum();
    }

    private sItem[] SearchBox(HashSet<int> ids) {
      return (Context.Player.ItemBox?.FindItemsInBox(ids) ?? new())
        .Select(
          i => new sItem() {
            ItemId = i.Key,
            Amount = i.Value,
          })
        .ToArray();
    }
    private sItem[] SearchPouch(HashSet<int> ids) {
      return Context.Player.Inventory?.FindItemsAndAmmos(ids);
    }
  }
}
