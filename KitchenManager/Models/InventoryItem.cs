using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace KitchenManager.Models
{
    [Table("curent_stock")]
    public class InventoryItem
    {
        [PrimaryKey]
        [Column("name")]
        public string ItemName { get; set; }
        [Column("stock_level")]
        public decimal StockLevel { get; set; }
        [Column("unit")]
        public string Unit {  get; set; }
        [Column("cost")]
        public decimal Cost { get; set; }

        public InventoryItem() { }

        public InventoryItem(string itemName, string unit, decimal stockLevel, decimal cost)
        {
            ItemName = itemName;
            Unit = unit;
            Cost = cost;
            StockLevel = stockLevel;
        }
    }
}
