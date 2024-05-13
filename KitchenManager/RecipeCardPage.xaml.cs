﻿using KitchenManager.Models;
using KitchenManager.Controllers;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace KitchenManager;

public partial class RecipeCardPage : ContentPage
{
    LocalDBService localDBService = new();

    RecipeManager manager = ((App)Application.Current).recipeManager;
    public RecipeCardPage()
    {
        InitializeComponent();
        PopulateFields(manager.CurrentRecipe);
    }

    async Task PopulateFields(Recipe recipe)
    {
        if (recipe.Ingredients == null || recipe.Label == null) { return; }

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

        // Calculate costs
        //TODO: Move to Controllers
        foreach (FoodItem ingredient in recipe.Ingredients)
        {
            InventoryItem? item = await localDBService.GetItem(ingredient.FoodName.ToLower());
            if (item != null)
            {
                ingredient.Cost = item.Cost / UnitHandler.GetCostUnits()[item.CostUnit] * ingredient.GramsWeight;
            }
            else { ingredient.Cost = 0; }
        }

        CollectionView_Ingredients.ItemsSource = recipe.Ingredients;

        
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
    }

    private async void Button_IncreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
        yield++;
        Label_RecipeYield.Text = $"{yield} People";

        await manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
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
        string message = "Recipe Saved.";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;

        var toast = Toast.Make(message, duration, fontSize);

        await localDBService.AddRecipe(manager.CurrentRecipe);
        await toast.Show(cancellationTokenSource.Token);

        ToggleButtons(false);
    }

    private async void Button_DeleteRecipe_Pressed(object sender, EventArgs e)
    {
        await localDBService.RemoveRecipe(manager.CurrentRecipe);
        ToggleButtons(true);
    }

    private void ToggleButtons(bool wasSaved)
    {
        Button_SaveRecipe.IsVisible = wasSaved ? true : false;
        Button_DeleteRecipe.IsVisible = wasSaved ? false: true;
    }
}