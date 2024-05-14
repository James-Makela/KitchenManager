using KitchenManager.Models;
using KitchenManager.Controllers;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace KitchenManager;

public partial class RecipeCardPage : ContentPage
{
    LocalDBService localDBService = ((App)Application.Current).localDBService;
    RecipeManager manager = ((App)Application.Current).recipeManager;

    public RecipeCardPage()
    {
        InitializeComponent();
        PopulateFields(manager.CurrentRecipe);
    }

    async Task PopulateFields(Recipe recipe)
    {
        if (recipe == null) 
        {
            // Fetch a random recipe
            Recipe randomRecipe = await manager.GetRandomRecipe();
            manager.CurrentRecipe = randomRecipe;
            recipe = manager.CurrentRecipe;
        }


        if (await localDBService.CheckRecipeIsSaved(recipe.Label))
        {
            Button_SaveRecipe.IsVisible = false;
            Button_DeleteRecipe.IsVisible = true;
        }
        int yield = PreferencesManager.GetPeople();

        await manager.ChangeYield(PreferencesManager.GetPeople());

        Label_RecipeLabel.Text = recipe.Label;
        Button_RecipeSource.Text = $"🔗 Source: {recipe.SourceName}";
        Label_RecipeYield.Text = $"{yield} People";
        Image_RecipeImage.Source = recipe.ImageLink;

        CollectionView_Ingredients.ItemsSource = recipe.Ingredients;
        DisplayTotals();
    }

    private async void Button_DecreaseYield_Pressed(object sender, EventArgs e)
    {
		int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
		if (yield == 1)
		{
			return;
		}
		yield--;
        Label_RecipeYield.Text = $"{yield} People";

        await manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
        DisplayTotals();
    }

    private async void Button_IncreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
        yield++;
        Label_RecipeYield.Text = $"{yield} People";

        await manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
        DisplayTotals();
    }

    private void DisplayTotals()
    {
        decimal totalCost = (decimal)manager.CurrentRecipe.TotalCost;
        decimal costPerServe = totalCost / (decimal)manager.CurrentRecipe.Yield;
        Label_TotalCost.Text = totalCost.ToString("C");
        Label_CostPerServe.Text = costPerServe.ToString("C");
    }

    private async void Button_RecipeSource_Pressed(object sender, EventArgs e)
    {
        try
        {
            string? url = manager.CurrentRecipe.SourceLink;
            if (url == null) { return; }
            Uri uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // TODO: handle exceptions
        }
    }

    private async void Button_SaveRecipe_Pressed(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new();
        string message = "Recipe Saved";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;

        var toast = Toast.Make(message, duration, fontSize);

        await localDBService.AddRecipe(manager.CurrentRecipe);
        await toast.Show(cancellationTokenSource.Token);

        ToggleButtons(false);
    }

    private async void Button_DeleteRecipe_Pressed(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new();
        string message = "Recipe Deleted";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;

        var toast = Toast.Make(message, duration, fontSize);

        await localDBService.RemoveRecipe(manager.CurrentRecipe);
        await toast.Show(cancellationTokenSource.Token);

        ToggleButtons(true);
    }

    private void ToggleButtons(bool wasSaved)
    {
        Button_SaveRecipe.IsVisible = wasSaved ? true : false;
        Button_DeleteRecipe.IsVisible = wasSaved ? false: true;
    }

    private void Button_Ingredients_Pressed(object sender, EventArgs e)
    {
        Border_TableView.IsVisible = true;
        Border_NutritionTableView.IsVisible = false;
    }

    private async void Button_Nutrition_Pressed(object sender, EventArgs e)
    {
        Border_TableView.IsVisible = false;
        Border_NutritionTableView.IsVisible = true;

        if (manager.NutritionInfo == null)
        {
            manager.NutritionInfo = await manager.GetNutrition();
        }

        CollectionView_Nutrition.ItemsSource = manager.NutritionInfo.NutritionItems;
    }
}