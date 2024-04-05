using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KitchenManager.Models
{
    public class FoodItem
    {
        [JsonProperty("foodId")]
        public string ID {  get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("measure")]
        public string Measure { get; set; }
        [JsonProperty("food")]
        public string FoodName { get; set; }
        [JsonProperty("weight")]
        public decimal GramsWeight { get; set; }
        [JsonProperty("foodCategory")]
        public string FoodType { get; set;}

        public FoodItem(string iD, decimal quantity, string measure, string foodName, decimal gramsWeight, string foodType)
        {
            ID = iD;
            Quantity = quantity;
            Measure = measure;
            FoodName = foodName;
            GramsWeight = gramsWeight;
            FoodType = foodType;
        }

        public override string ToString()
        {
            if (Measure == "<unit>")
            {
                return $"{Quantity.ToString("0")} {FoodName}";
            }
            return $"{Quantity.ToString("0")} {Measure} {FoodName}";
        }
    }

    
}
