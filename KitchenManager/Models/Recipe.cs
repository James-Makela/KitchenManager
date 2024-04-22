using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    [Serializable]
    public class Recipe
    {

        [JsonProperty("uri")]
        public string? URI { get; set; }
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("image")]
        public string? ImageLink { get; set; }
        [JsonProperty("source")]
        public string? SourceName { get; set; }
        [JsonProperty("url")]
        public string? SourceLink { get; set; }
        [JsonProperty("yield")]
        public decimal? Yield {  get; set; }
        [JsonProperty("healthLabels")]
        public List<string>? HealthTags { get; set; }
        [JsonProperty("cautions")]
        public List<string>? FoodCautions { get; set; }
        [JsonProperty("ingredients")]
        public List<FoodItem>? Ingredients {  get; set; }
        [JsonProperty("cuisineType")]
        public List<string>? CuisineTypeList {  get; set; }

        string? _cuisineType;
        public string CuisineType
        {
            get
            {
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                string cuisine = CuisineTypeList[0];
                cuisine = ti.ToTitleCase(cuisine);
                return cuisine;
            }
        }

        public Recipe() { }
    }
}
