using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenManager.Models
{
    internal class FoodItem
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // TODO: Do I want to create an enum for unit possibilities?
        public string PriceUnit { get; set; }

        // TODO: How will this initialise - will this start at 0?
        public int CurrentStock { get; set; }

        public FoodItem(string iD, string name, decimal price, string priceUnit, int currentStock)
        {
            ID = iD;
            Name = name;
            Price = price;
            PriceUnit = priceUnit;
            CurrentStock = currentStock;
        }
    }

    
}
