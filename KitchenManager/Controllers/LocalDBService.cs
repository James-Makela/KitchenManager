using SQLite;
using System;
using System.Collections.Generic;
using SQLiteNetExtensionsAsync.Extensions;
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
            var tables = _connection.CreateTablesAsync<InventoryItem, Recipe, FoodItem>();
            Console.WriteLine(tables);
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

        public async Task AddRecipe(Recipe recipe)
        {
            // TODO: Add error handling for recipe already saved
            await _connection.InsertWithChildrenAsync(recipe);
        }

        public async Task<List<Recipe>>? GetSavedRecipes()
        {
            List<Recipe> items = new List<Recipe>();
            try
            {
                items = await _connection.GetAllWithChildrenAsync<Recipe>();
            }
            catch (Exception ex)
            {
                return items;
            }
            return items;
        }
    }
}
