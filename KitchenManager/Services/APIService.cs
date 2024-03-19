using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KitchenManager.Models;

namespace KitchenManager.Services
{
    internal class APIService
    {
        public APIService() { }

        public async Task<Recipe> GetRecipe(SearchQuery searchQuery)
        {
            HttpClient client = new();

            APIKeys? keys = JsonConvert.DeserializeObject<APIKeys>(File.ReadAllText("Keys/keys.json"));
            string baseURL = "https://api.edamam.com/api/recipes/v2?type=public";
        }

    }
}
