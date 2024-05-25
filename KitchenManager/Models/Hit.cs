using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class Hit
    {
        [JsonProperty("recipe")]
        public Recipe? Recipe { get; set; }
    }
}
