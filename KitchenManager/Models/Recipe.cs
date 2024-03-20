﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    internal class Recipe
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
        public int? Yield {  get; set; }
        [JsonProperty("healthLabels")]
        public List<string>? HealthTags { get; set; }
        [JsonProperty("cautions")]
        public List<string>? FoodCautions { get; set; }
        [JsonProperty("ingredients")]
        public List<FoodItem>? Ingredients {  get; set; }

        public Recipe() { }

        public override string ToString()
        {
            return Label;
        }
    }
}
