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
            CollectionView_Recipes.ItemsSource = recipes;
        }
	}

    private async void CollectionView_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Recipe selectedRecipe = (Recipe)CollectionView_Recipes.SelectedItem;

        if (selectedRecipe != null)
        {
            RecipeCardPage recipeCardPage = new RecipeCardPage(selectedRecipe);
            await Navigation.PushModalAsync(recipeCardPage);
            ((CollectionView)CollectionView_Recipes).SelectedItem = null;
        }
    }

    private void Button_SavedRecipes_Pressed(object sender, EventArgs e)
    {
        Border_SavedRecipes.Stroke = Color.Parse("#415a77");
        Border_NewRecipes.Stroke = Color.Parse("#d9d9d9");
        Border_SavedRecipes.Background = Color.Parse("#415a77");
        Border_NewRecipes.Background = Color.Parse("#d9d9d9");
    }

    private void Button_NewRecipes_Pressed(object sender, EventArgs e)
    {
        Border_NewRecipes.Stroke = Color.Parse("#415a77");
        Border_SavedRecipes.Stroke = Color.Parse("#d9d9d9");
        Border_NewRecipes.Background = Color.Parse("#415a77");
        Border_SavedRecipes.Background = Color.Parse("#d9d9d9");
    }
}