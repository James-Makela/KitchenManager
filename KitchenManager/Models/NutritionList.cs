using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class NutritionList
    {
        [JsonProperty("hits")]
        public List<NutritionHit>? Results { get; set; }
    }
}
