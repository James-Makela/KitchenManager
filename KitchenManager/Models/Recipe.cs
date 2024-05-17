using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace KitchenManager.Models
{
    [Table("saved_recipes")]
    [Serializable]
    public class Recipe
    {

        [PrimaryKey]
        [Column("uri")]
        [JsonProperty("uri")]
        public string? URI { get; set; }
        [Column("label")]
        [JsonProperty("label")]
        public string? Label { get; set; }
        [Column("image")]
        [JsonProperty("image")]
        public string? ImageLink { get; set; }
        [Column("source_name")]
        [JsonProperty("source")]
        public string? SourceName { get; set; }
        [Column("source_link")]
        [JsonProperty("url")]
        public string? SourceLink { get; set; }
        [Column("yield")]
        [JsonProperty("yield")]
        public decimal Yield { get; set; }
        [TextBlob(nameof(HealthTagsBlobbed))]
        [JsonProperty("healthLabels")]
        public List<string>? HealthTags { get; set; }
        [TextBlob(nameof(FoodCautionsBlobbed))]
        [JsonProperty("cautions")]
        public List<string>? FoodCautions { get; set; }
        [TextBlob(nameof(IngredientsBlobbed))]
        [JsonProperty("ingredients")]
        public List<FoodItem>? Ingredients { get; set; }
        [TextBlob(nameof(CuisineTypeListBlobbed))]
        [JsonProperty("cuisineType")]
        public List<string>? CuisineTypeList { get; set; }
        public decimal? TotalCost { get; set; }
        public string HealthTagsBlobbed { get; set; }
        public string FoodCautionsBlobbed { get; set; }
        public string CuisineTypeListBlobbed { get; set; }
        public string IngredientsBlobbed { get; set; }

        public string? CuisineType
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
