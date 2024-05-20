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

        public Recipe? CurrentRecipe { get; set; }
        public List<NutritionItem>? NutritionInfo { get; set; }

        int OriginalYield { get; set; }

        public RecipeManager() { }

        public async Task ChangeYield(int newYield)
        {
            int oldYield = Convert.ToInt32(CurrentRecipe.Yield);
            OriginalYield = oldYield;
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

        public void RunConversions()
        {
            if (CurrentRecipe.Ingredients == null) { return; }

            foreach (FoodItem ingredient in CurrentRecipe.Ingredients)
            {
                Tuple<decimal, string> newMeasurement = UnitConverter.Convert(ingredient.GramsWeight, ingredient.Measure);
                if (newMeasurement == null)
                {
                    continue;
                }
                ingredient.Quantity = newMeasurement.Item1;
                ingredient.Measure = newMeasurement.Item2;
            }
        }

        public async Task<Recipe> GetRandomRecipe()
        {
            Recipe recipe = await apiService.GetRandom();
            return recipe;
        }

        public async Task<List<NutritionItem>?> GetNutrition()
        {
            if (CurrentRecipe.URI == null)
            {
                // TODO: Add error handling here
                return null;
            }
            List<NutritionItem> nutritionItems = new();

            NutritionInfo nutritionInfo = await apiService.GetNutrition(CurrentRecipe.URI);

            foreach (NutritionItem nutritionItem in nutritionInfo.NutritionItems)
            {
                if (nutritionItem == null) { return null; }

                nutritionItem.TotalAmount = nutritionItem.TotalAmount / OriginalYield;
                nutritionItem.DailyPercentage = nutritionItem.DailyPercentage / OriginalYield;

                nutritionItems.Add(nutritionItem);
                if (nutritionItem.SubItems != null)
                {
                    foreach (NutritionItem subItem in nutritionItem.SubItems)
                    {
                        subItem.Label = $"- {subItem.Label}";
                        subItem.TotalAmount = subItem.TotalAmount / OriginalYield;
                        subItem.DailyPercentage = subItem.DailyPercentage / OriginalYield;
                        nutritionItems.Add(subItem);
                    }
                }
            }

            return nutritionItems;
        }
    }
}
