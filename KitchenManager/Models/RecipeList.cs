using Newtonsoft.Json;

namespace KitchenManager.Models
{
    internal class RecipeList
    {
        [JsonProperty("from")]
        public int From { get; set; }
        [JsonProperty("to")]
        public int To { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("hits")]
        public List<Hit>? Results { get; set; }
    }
}
