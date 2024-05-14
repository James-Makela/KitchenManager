using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenManager.Models;

namespace KitchenManager.Controllers
{
    public class RecipeManager
    {
        APIService apiService = new();
        LocalDBService localDBService = new();
        public Recipe CurrentRecipe { get; set; }
        public NutritionInfo NutritionInfo { get; set; }

        public RecipeManager() { }

        public async Task ChangeYield(int newYield)
        {
            int oldYield = Convert.ToInt32(CurrentRecipe.Yield);
            CurrentRecipe.Yield = newYield;
            List<FoodItem> ingredients = new();
            await Task.Run(() =>
            {
                foreach (FoodItem ingredient in CurrentRecipe.Ingredients)
                {
                    decimal oldQuantity = ingredient.Quantity;
                    ingredient.Quantity = (oldQuantity / oldYield) * newYield;

                    decimal oldGrams = ingredient.GramsWeight;
                    ingredient.GramsWeight = (oldGrams / oldYield) * newYield;
                }
            });
            await CalculateCosts();
        }

        public async Task CalculateCosts()
        {
            decimal? totalCost = 0;
            foreach (FoodItem ingredient in CurrentRecipe.Ingredients)
            {
                InventoryItem? item = await localDBService.GetItem(ingredient.FoodName.ToLower());
                if (item != null)
                {
                    ingredient.Cost = item.Cost / UnitHandler.GetCostUnits()[item.CostUnit] * ingredient.GramsWeight;
                    totalCost += ingredient.Cost;
                }
                else { ingredient.Cost = 0; }
            }
            CurrentRecipe.TotalCost = totalCost;
        }

        public async Task<Recipe> GetRandomRecipe()
        {
            Recipe recipe = await apiService.GetRandom();
            return recipe;
        }

        public async Task<NutritionInfo>? GetNutrition()
        {
            if (CurrentRecipe.URI == null)
            {
                // TODO: Add error handling here
                return null;
            }
            NutritionInfo nutritionInfo = await apiService.GetNutrition(CurrentRecipe.URI);
            return nutritionInfo;
        }
    }
}
