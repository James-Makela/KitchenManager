using KitchenManager.Models;
using KitchenManager.Services;

namespace KitchenManager;

public partial class RecipesPage : ContentPage
{
	public RecipesPage()
	{
		InitializeComponent();
		PopulateRecipes();

		// Testing code
		// ------------
		async void PopulateRecipes()
		{
            APIService service = new APIService();
            RecipeSearchQuery query = new RecipeSearchQuery("chicken", ["alcohol-free", "dairy-free"], ["Dinner"]);

            List<Recipe> recipes = await service.GetRecipes(query);
            ListView_Recipes.ItemsSource = recipes;

        }
	}
}