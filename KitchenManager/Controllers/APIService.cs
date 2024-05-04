using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KitchenManager.Models;

namespace KitchenManager.Controllers
{
    internal class APIService
    {
        const string baseURL = "https://api.edamam.com/api/recipes/v2?type=public";

        public APIService() { }

        public async Task<List<Recipe>>? GetRecipes(RecipeSearchQuery searchQuery)
        {
            RecipeList? recipeResults = new();
            List<Recipe>? recipes = new();

            using HttpClient client = new();
            string fullUrl = await GetSymbols(searchQuery);

            HttpResponseMessage response = await client.GetAsync(fullUrl);

            JsonSerializer serializer = new();

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType ==  JsonToken.StartObject)
                    {
                        recipeResults = serializer.Deserialize<RecipeList>(jsonReader);
                    }
                }
            }

            foreach (Hit result in recipeResults.Results)
            {
                recipes.Add(result.Recipe);
            }

            return recipes;
        }

        public async Task<string> GetSymbols(RecipeSearchQuery query)
        {
            string keysJson = await ReadTextFile("keys.json");
            APIKeys? accessKeys = JsonConvert.DeserializeObject<APIKeys>(keysJson);

            // TODO: Further implement a key error notification
            // This must be fed through to the page that calls the function that calls this
            if (accessKeys == null)
            {
                return "keyError";
            }

            string fullURL = baseURL;
            if (query.Query != null)
            {
                fullURL += $"&q={query.Query}";
            }
            else
            {
                fullURL += "&random=true";
            }

            fullURL += $"&app_id={accessKeys.app_id}";
            fullURL += $"&app_key={accessKeys.api_key}";
            fullURL += "&ingr=5%2B"; // Only return recipes with at least 5 ingredients

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
