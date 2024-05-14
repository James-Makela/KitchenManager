using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenManager.Models
{
    internal class SearchQuery
    {
        public string QueryType { get; set; }
        public string? Query { get; set; }
        public string? URI { get; set; }
        public List<string>? HealthTags { get; set; }
        public List<string>? MealType { get; set; }
        public List<string>? ReturnFields { get; set; }

        public List<string>? NutritionReturnFields { get; set; }

        public SearchQuery(string queryType="recipe", string? query=null, List<string>? healthTags=null, List<string>? mealType=null)
        {
            QueryType = queryType;
            Query = query;
            HealthTags = healthTags;
            MealType = mealType;
            ReturnFields = ["uri",
                            "label",
                            "image",
                            "source",
                            "url",
                            "yield",
                            "healthLabels",
                            "cautions",
                            "ingredients",
                            "cuisineType"];
            NutritionReturnFields = ["calories",
                                     "glycemicIndex",
                                     "inflammatoryIndex",
                                     "totalCO2Emissions",
                                     "digest"];
        }
    }
}
