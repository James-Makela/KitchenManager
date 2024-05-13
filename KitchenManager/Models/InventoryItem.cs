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
        private string itemName;

        [PrimaryKey]
        [Column("name")]
        public string ItemName { get => itemName; set => itemName = value.ToLower(); }
        [Column("stock_level")]
        public decimal StockLevel { get; set; }
        [Column("stockunit")]
        public string StockUnit { get; set; }
        [Column("costunit")]
        public string CostUnit { get; set; }
        [Column("cost")]
        public decimal Cost { get; set; }

        public InventoryItem() { }

        public InventoryItem(string itemName, string stockUnit, string costUnit, decimal stockLevel, decimal cost)
        {
            ItemName = itemName;
            StockUnit = stockUnit;
            CostUnit = costUnit;
            Cost = cost;
            StockLevel = stockLevel;
        }
    }
}
