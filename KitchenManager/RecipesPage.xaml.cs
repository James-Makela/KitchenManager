using KitchenManager.Models;
using KitchenManager.Controllers;
using KitchenManager.Controllers;

namespace KitchenManager;

public partial class RecipesPage : FramePage
{
    List<Recipe>? recipes;
    List<Recipe>? savedRecipes;
    LocalDBService localDBService = new();
    bool viewingSaved = false;

    public RecipesPage()
    {
        InitializeComponent();
        Button button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");
        button_LeftTab.Text = "New";
        button_RightTab.Text = "Saved";
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (recipes == null)
        {
            await PopulateRecipes();
        }
        savedRecipes = await localDBService.GetSavedRecipes();
        ActivityIndicator_Loading.IsRunning = false;
        CollectionView_Recipes.IsVisible = true;
    }

    // Testing code
    // ------------
    async Task PopulateRecipes()
    {
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
            RecipeCardPage recipeCardPage = new RecipeCardPage(selectedRecipe, viewingSaved);
            await Navigation.PushModalAsync(recipeCardPage);
            CollectionView_Recipes.SelectedItem = null;
        }
    }

    protected override void LeftTab_Pressed()
    {
        base.LeftTab_Pressed();
        viewingSaved = false;
        CollectionView_Recipes.ItemsSource = recipes;
    }

    protected override void RightTab_Pressed()
    {
        base.RightTab_Pressed();
        viewingSaved = true;
        CollectionView_Recipes.ItemsSource = savedRecipes;
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