using Newtonsoft.Json;
using KitchenManager.Models;
using System.Web;

namespace KitchenManager.Controllers
{
    internal class APIService
    {
        const string baseURL = "https://api.edamam.com/api/recipes/v2?type=public";
        const string nutritionURL = " https://api.edamam.com/api/recipes/v2/by-uri?type=public";

        public APIService() { }

        public async Task<List<Recipe>>? GetRecipes(SearchQuery searchQuery, int numbertoReturn=20)
        {
            RecipeList recipeResults = new();
            List<Recipe>? recipes = new();

            using HttpClient client = new();
            string fullUrl = await GetUrl(searchQuery);

            if (fullUrl.StartsWith("Error"))
            {
                return ReturnError("API");
            }

            HttpResponseMessage response = await client.GetAsync(fullUrl);

            JsonSerializer serializer = new();

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                if (numbertoReturn == 1)
                {
                    jsonReader.Read();
                    recipeResults = serializer.Deserialize<RecipeList>(jsonReader);
                    Hit hit = recipeResults.Results[0];
                    recipes.Add(hit.Recipe);
                    return recipes;
                }
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType ==  JsonToken.StartObject)
                    {
                        recipeResults = serializer.Deserialize<RecipeList>(jsonReader);
                    }
                }
            }
            if (recipeResults == null || recipeResults.Results == null)
            {
                return ReturnError("No Results");
            }

            foreach (Hit result in recipeResults.Results)
            {
                recipes.Add(result.Recipe);
            }

            return recipes;
        }

        public async Task<NutritionInfo> GetNutrition(string recipeURI)
        {
            SearchQuery searchQuery = new()
            {
                QueryType = "nutrition",
                URI = HttpUtility.UrlEncode(recipeURI)
            };

            using HttpClient client = new();
            string fullUrl = await GetUrl(searchQuery);

            HttpResponseMessage response = await client.GetAsync(fullUrl);

            JsonSerializer serializer = new();

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                jsonReader.Read();
                NutritionList nutritionResults = serializer.Deserialize<NutritionList>(jsonReader);
                NutritionHit nutritionHit = nutritionResults.Results[0];
                return nutritionHit.NutritionInfo;
            }
        }

        public async Task<Recipe> GetRandom()
        {
            Recipe recipe = new();
            SearchQuery searchQuery = new();
            List<Recipe> singleRecipe = await GetRecipes(searchQuery, 1);
            recipe = singleRecipe[0];


            return recipe;
        }

        public async Task<string> GetUrl(SearchQuery query, int returns=0)
        {
            string keysJson;
            APIKeys? accessKeys;
            try
            {
                keysJson = await ReadKeysFile("keys.json");
                accessKeys = JsonConvert.DeserializeObject<APIKeys>(keysJson);
            }
            catch (FileNotFoundException)
            {
                return "Error: API keys not configured.";
            }

            string fullURL = baseURL;

            if (query.QueryType == "nutrition")
            {
                fullURL = nutritionURL;
                fullURL += $"&uri={query.URI}";

                fullURL += $"&app_id={accessKeys.app_id}";
                fullURL += $"&app_key={accessKeys.api_key}";

                foreach (string field in query.NutritionReturnFields)
                {
                    fullURL += $"&field={field}";
                }

                return fullURL;

            }

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

        public async Task<string> ReadKeysFile(string path)
        {
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(path);
            using StreamReader reader = new StreamReader(fileStream);
            return await reader.ReadToEndAsync();
        }

        public List<Recipe> ReturnError(string message, string details="")
        {
            Recipe errorMessage = new Recipe
            {
                Label = $"Error: {message}",
                CuisineTypeList = [$"{details}"]
            };
            List<Recipe> error = [errorMessage];
            return error;
        }

    }
}
