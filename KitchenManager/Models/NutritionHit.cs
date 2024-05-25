using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class NutritionHit
    {
        [JsonProperty("recipe")]
        public NutritionInfo? NutritionInfo { get; set; }
    }
}
