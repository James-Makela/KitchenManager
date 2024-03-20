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
        APIKeys? accessKeys = JsonConvert.DeserializeObject<APIKeys>(File.ReadAllText("Keys/keys.json"));
        const string baseURL = "https://api.edamam.com/api/recipes/v2?type=public";

        public APIService() { }

        public async Task<List<Recipe>> GetRecipes(RecipeSearchQuery searchQuery)
        {
            HttpClient client = new();

            string fullURL = URLConstructor(searchQuery);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURL);

            HttpResponseMessage response = await client.SendAsync(request);

            string responseString = await response.Content.ReadAsStringAsync();

            // newtonsoft serialising json fragments
            JObject recipeResults = JObject.Parse(responseString);
            IList<JToken> results = recipeResults["hits"]["recipe"].Children().ToList();

            List<Recipe> recipes = new List<Recipe>();
            foreach (JToken recipeResult in results)
            {
                Recipe recipe = recipeResult.ToObject<Recipe>();
                recipes.Add(recipe);
            }

            return recipes;
        }

        public string URLConstructor(RecipeSearchQuery query)
        {
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

    }
}
