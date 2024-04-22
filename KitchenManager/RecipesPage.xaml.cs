using KitchenManager.Models;
using KitchenManager.Services;

namespace KitchenManager;

public partial class RecipesPage : FramePage
{
    List<Recipe> recipes;

    public RecipesPage(string[] labelStrings) : base(labelStrings)
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        // MAUI android quirk
        await Task.Delay(500);
        await PopulateRecipes();
    }

    // Testing code
    // ------------
    async Task PopulateRecipes()
    {
        recipes = await FetchRecipes();
        CollectionView_Recipes.ItemsSource = recipes;
    }

    async Task<List<Recipe>> FetchRecipes()
    {
        APIService service = new APIService();
        RecipeSearchQuery query = new RecipeSearchQuery("Chicken", ["alcohol-free", "dairy-free"], ["Dinner"]);

        return await service.GetRecipes(query);
    }
    // -------------------
    // End of testing code


    /// <summary>
    /// Opens the recipe card for the selected recipe upon selection.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void CollectionView_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Recipe selectedRecipe = (Recipe)CollectionView_Recipes.SelectedItem;

        if (selectedRecipe != null)
        {
            RecipeCardPage recipeCardPage = new RecipeCardPage(selectedRecipe);
            await Navigation.PushModalAsync(recipeCardPage);
            CollectionView_Recipes.SelectedItem = null;
        }
    }

    protected override void LeftTab_Pressed()
    {
        base.LeftTab_Pressed();
        CollectionView_Recipes.ItemsSource = recipes;
    }

    protected override void RightTab_Pressed()
    {
        base.RightTab_Pressed();
        CollectionView_Recipes.ItemsSource = null;
    }
}