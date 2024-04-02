using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    class Hit
    {
        [JsonProperty("recipe")]
        public Recipe? Recipe {  get; set; }
    }
}
