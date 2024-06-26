﻿using Newtonsoft.Json;

namespace KitchenManager.Models
{
    [Serializable]
    public class NutritionItem
    {
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("tag")]
        public string? NutritionTag { get; }
        [JsonProperty("total")]
        public decimal TotalAmount { get; set; }
        [JsonProperty("hasRDI")]
        public bool? HasRDI { get; set; }
        [JsonProperty("daily")]
        public decimal? DailyPercentage { get; set; }
        [JsonProperty("unit")]
        public string? Unit { get; set; }
        [JsonProperty("sub")]
        public List<NutritionItem>? SubItems { get; set; }

        public string TotalWithUnits
        {
            get =>
                $"{TotalAmount.ToString("0")} {Unit}";
        }
    }
}
