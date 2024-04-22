using KitchenManager.Models;
using KitchenManager.Services;

namespace KitchenManager;

public partial class RecipesPage : FramePage
{
    List<Recipe>? recipes;

    public RecipesPage(string[] labelStrings) : base(labelStrings)
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (recipes == null)
        {
            await PopulateRecipes();
        }
    }

    // Testing code
    // ------------
    async Task PopulateRecipes()
    {
        recipes = null;
        recipes = await FetchRecipes();
        CollectionView_Recipes.ItemsSource = recipes;
    }

    async Task<List<Recipe>> FetchRecipes(string query="Blueberry")
    {
        APIService service = new APIService();
        RecipeSearchQuery searchQuery = new RecipeSearchQuery(query, ["alcohol-free", "dairy-free"], ["Dinner"]);

        return await service.GetRecipes(searchQuery);
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

    private async void SearchBar_SearchBox_SearchButtonPressed(object sender, EventArgs e)
    {
        string searchText = SearchBar_SearchBox.Text;

        if (SearchBar_SearchBox.IsSoftInputShowing())
        {
            await SearchBar_SearchBox.HideSoftInputAsync(System.Threading.CancellationToken.None);
        }

        if (!string.IsNullOrEmpty(searchText))
        {
            CollectionView_Recipes.ItemsSource = null;
            recipes = await FetchRecipes(searchText);
            CollectionView_Recipes.ItemsSource = recipes;
        }
    }
}