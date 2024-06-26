﻿using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
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

        // Inventory Functions
        public async Task<List<InventoryItem>>? GetInventory()
        {
            List<InventoryItem> items = new List<InventoryItem>();
            try
            {
                items = await _connection.Table<InventoryItem>().ToListAsync();
            }
            catch (Exception)
            {
                return items;
            }
            return items;
        }

        public async Task<InventoryItem> GetItem(string name)
        {
            return await _connection.Table<InventoryItem>().Where(x => x.ItemName == name).FirstOrDefaultAsync();
        }

        public async Task<bool> AddInventoryItem(InventoryItem item)
        {
            try
            {
                await _connection.InsertAsync(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditInventoryItem(InventoryItem changedItem)
        {
            try
            {
                await _connection.InsertOrReplaceAsync(changedItem);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<InventoryItem?> FindInventoryItem(string itemName)
        {
            try
            {
                return await _connection.GetAsync<InventoryItem>(itemName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> RemoveInventoryItem(InventoryItem itemToDelete)
        {
            try
            {
                return await _connection.DeleteAsync(itemToDelete);
            }
            catch (Exception)
            {
                return 0;
            }
        }



        // Recipe Functions
        public async Task AddRecipe(Recipe recipe)
        {
            // TODO: Add error handling for recipe already saved
            try
            {
                await _connection.InsertWithChildrenAsync(recipe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            await _connection.DeleteAsync(recipe);
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
                throw new Exception(ex.Message);
            }
            return items;
        }

        public async Task<bool> CheckRecipeIsSaved(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName)) { return false; }

            try
            {
                Recipe savedRecipe = await _connection.Table<Recipe>().Where(recipe => recipe.Label == recipeName).FirstOrDefaultAsync();
                if (savedRecipe != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
