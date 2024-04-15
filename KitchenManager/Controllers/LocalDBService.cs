using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenManager.Models;

namespace KitchenManager.Controllers
{
    public class LocalDBService
    {
        private const string DB_NAME = "kitchen_manager_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<InventoryItem>();
        }

        public async Task<List<InventoryItem>>? GetInventory()
        {
            List<InventoryItem> items = new List<InventoryItem>();
            try
            {
                items = await _connection.Table<InventoryItem>().ToListAsync();
            }
            catch (Exception ex)
            {
                return items;
            }
            return items;
        }

        public async Task<InventoryItem> GetItem(string name)
        {
            return await _connection.Table<InventoryItem>().Where(x => x.ItemName == name).FirstOrDefaultAsync();
        }

        public async Task AddInventoryItem(InventoryItem item)
        {
            await _connection.InsertAsync(item);
        }
    }
}
