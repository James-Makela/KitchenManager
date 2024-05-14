using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class NutritionList
    {
        [JsonProperty("hits")]
        public List<NutritionHit>? Results { get; set; }
    }
}
