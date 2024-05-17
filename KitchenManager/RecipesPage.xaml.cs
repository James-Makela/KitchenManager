using KitchenManager.Models;
using KitchenManager.Controllers;

namespace KitchenManager;

public partial class RecipesPage : FramePage
{
    List<Recipe>? recipes;
    List<Recipe>? savedRecipes;
    LocalDBService localDBService = ((App)Application.Current).localDBService;
    RecipeManager recipeManager = ((App)Application.Current).recipeManager;
    bool viewingSaved = false;

    public RecipesPage()
    {
        InitializeComponent();
        Button button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");
        button_LeftTab.Text = "New";
        button_RightTab.Text = "Saved";
    }

    private async void ContentView_Loaded(object sender, EventArgs e)
    {
        if (recipes == null)
        {
            await PopulateRecipes();
        }
        savedRecipes = await localDBService.GetSavedRecipes();
        ActivityIndicator_Loading.IsRunning = false;
        CollectionView_Recipes.IsVisible = true;
        CollectionView_Recipes.SelectedItem = null;

        if (viewingSaved)
        {
            CollectionView_Recipes.ItemsSource = savedRecipes;
        }
    }

    async Task PopulateRecipes(string? query=null)
    {
        APIService service = new();
        SearchQuery searchQuery = new SearchQuery(query);

        try
        {
            recipes = await service.GetRecipes(searchQuery);
        }
        catch (Exception ex)
        {
            await DisplayError(ex);
        }
        CollectionView_Recipes.ItemsSource = recipes;
    }

    private async Task DisplayError(Exception ex)
    {
        await DisplayAlert("Error", ex.Message, "Ok");
    }

    private async void CollectionView_Recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        recipeManager.CurrentRecipe = (Recipe)CollectionView_Recipes.SelectedItem;

        if (recipeManager.CurrentRecipe != null)
        {
            await Shell.Current.GoToAsync($"recipecard");
        }
    }

    protected override void LeftTab_Pressed()
    {
        viewingSaved = false;
        base.LeftTab_Pressed();
        CollectionView_Recipes.ItemsSource = recipes;
    }

    protected override void RightTab_Pressed()
    {
        viewingSaved = true;
        base.RightTab_Pressed();
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
            ActivityIndicator_Loading.IsRunning = true;
            CollectionView_Recipes.ItemsSource = null;
            await PopulateRecipes(searchText);
            ActivityIndicator_Loading.IsRunning = false;
        }
    }

    private async void RefreshView_Recipes_Refreshing(object sender, EventArgs e)
    {
        if (!viewingSaved)
        {
            await PopulateRecipes();
            RefreshView_Recipes.IsRefreshing = false;
        }
        else
        {
            CollectionView_Recipes.ItemsSource = savedRecipes;
            RefreshView_Recipes.IsRefreshing = false;
        }
    }
}