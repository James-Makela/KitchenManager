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
        private string measure;

        [JsonProperty("foodId")]
        public string ID {  get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("measure")]
        // Converting "<unit>" to "ea" where necessary
        public string Measure { get => measure; set => measure = value == "<unit>" ? "ea" : value; }
        [JsonProperty("food")]
        public string FoodName { get; set; }
        [JsonProperty("weight")]
        public decimal GramsWeight { get; set; }
        [JsonProperty("foodCategory")]
        public string FoodType { get; set; }
        public decimal? Cost { get; set; }


        public FoodItem() { }

        public FoodItem(string iD, decimal quantity, string measure, string foodName, decimal gramsWeight, string foodType, decimal cost=0)
        {
            ID = iD;
            Quantity = quantity;
            Measure = measure;
            FoodName = foodName;
            GramsWeight = gramsWeight;
            FoodType = foodType;
            Cost = cost;
        }

        public string QuantityMeasure { get =>
            $"{Quantity.ToString("0.##")} {Measure}";
        }

        //public override string ToString()
        //{
        //    if (Measure == "<unit>")
        //    {
        //        return $"{Quantity.ToString("0")} {FoodName}";
        //    }
        //    return $"{Quantity.ToString("0")} {Measure} {FoodName}";
        //}
    }

    
}
