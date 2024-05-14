using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class NutritionHit
    {
        [JsonProperty("recipe")]
        public NutritionInfo? NutritionInfo { get; set; }
    }
}
