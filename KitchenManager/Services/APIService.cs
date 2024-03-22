using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KitchenManager.Models;

namespace KitchenManager.Services
{
    internal class APIService
    {
        const string baseURL = "https://api.edamam.com/api/recipes/v2?type=public";

        public APIService() { }

        public async Task<List<Recipe>>? GetRecipes(RecipeSearchQuery searchQuery)
        {
            HttpClient client = new();
            string fullURL = await URLConstructor(searchQuery);

            JObject recipeResults;
            using (Stream stream = client.GetStreamAsync(fullURL).Result)
            using (StreamReader reader = new StreamReader(stream))
            {
                recipeResults = JObject.Parse(await reader.ReadToEndAsync());
            }

            IList<JToken> results = recipeResults["hits"].Children().ToList();

            List<Recipe> recipes = new List<Recipe>();
            foreach (JToken recipeResult in results)
            {
                JToken singleRecipe = recipeResult.SelectToken("recipe");
                Recipe recipe = singleRecipe.ToObject<Recipe>();
                recipes.Add(recipe);
            }

            return recipes;
        }

        public async Task<string> URLConstructor(RecipeSearchQuery query)
        {
            string keysJson = await ReadTextFile("keys.json");
            APIKeys? accessKeys = JsonConvert.DeserializeObject<APIKeys>(keysJson);
            string fullURL = baseURL;
            if (query.Query != null)
            {
                fullURL += $"&q={query.Query}";
            }

            fullURL += $"&app_id={accessKeys.app_id}";
            fullURL += $"&app_key={accessKeys.api_key}";

            if (query.HealthTags != null)
            {
                foreach (string tag in query.HealthTags)
                {
                    fullURL += $"&health={tag}";
                }
            }

            if (query.MealType != null)
            {
                foreach (string mealType in  query.MealType)
                {
                    fullURL += $"&mealType={mealType}";
                }
            }

            foreach (string field in query.ReturnFields)
            {
                fullURL += $"&field={field}";
            }

            return fullURL;
        }

        public async Task<string> ReadTextFile(string path)
        {
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(path);
            using StreamReader reader = new StreamReader(fileStream);
            return await reader.ReadToEndAsync();
        }

    }
}
