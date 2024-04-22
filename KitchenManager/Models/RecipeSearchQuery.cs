using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenManager.Models
{
    internal class RecipeSearchQuery
    {
        public string? Query { get; set; }
        public List<string>? HealthTags { get; set; }
        public List<string>? MealType { get; set; }
        public List<string>? ReturnFields { get; set; }

        public RecipeSearchQuery(string? query, List<string>? healthTags, List<string>? mealType)
        {
            Query = query;
            HealthTags = healthTags;
            MealType = mealType;
            ReturnFields = ["uri", "label", "image", "source", "url", "yield", "healthLabels", "cautions", "ingredients", "cuisineType"];
        }
    }
}
