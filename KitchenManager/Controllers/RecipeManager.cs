using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenManager.Models;

namespace KitchenManager.Controllers
{
    class RecipeManager
    {
        public Recipe CurrentRecipe { get; set; }

        public RecipeManager() { }

        public void ChangeYield(int newYield)
        {
            int oldYield = Convert.ToInt32(CurrentRecipe.Yield);
            CurrentRecipe.Yield = newYield;
            List<FoodItem> ingredients = new();
            foreach (FoodItem ingredient in CurrentRecipe.Ingredients)
            {
                decimal oldQuantity = ingredient.Quantity;
                ingredient.Quantity = (oldQuantity / oldYield) * newYield;
            }

        }
    }
}
