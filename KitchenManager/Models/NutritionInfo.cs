using Newtonsoft.Json;

namespace KitchenManager.Models
{
    public class NutritionInfo
    {
        [JsonProperty("calories")]
        public decimal Calories { get; set; }
        [JsonProperty("totalCO2Emissions")]
        public decimal Co2Emissions { get; set; }
        [JsonProperty("digest")]
        public List<NutritionItem>? NutritionItems { get; set; }
    }
}
