﻿using KitchenManager.Models;
using KitchenManager.Controllers;
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


        DeviceDisplay.Current.MainDisplayInfoChanged += (sender, args) =>
        {
            CollectionView_Ingredients.ItemsSource = null;
            CollectionView_Nutrition.ItemsSource = null;

            CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
            CollectionView_Nutrition.ItemsSource = manager.NutritionInfo;
        };
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        manager.CurrentRecipe = null;
        manager.NutritionInfo = null;
    }

    async Task PopulateFields(Recipe recipe)
    {
        if (recipe == null)
        {
            Recipe randomRecipe = await manager.GetRandomRecipe();
            manager.CurrentRecipe = randomRecipe;
            recipe = manager.CurrentRecipe;
        }

        manager.RunConversions();

        if (recipe.Label != null && await localDBService.CheckRecipeIsSaved(recipe.Label))
        {
            Button_SaveRecipe.IsVisible = false;
            Button_DeleteRecipe.IsVisible = true;
        }

        Title = recipe.Label;

        int yield = PreferencesManager.GetPeople();

        await manager.ChangeYield(PreferencesManager.GetPeople());

        Label_RecipeLabel.Text = recipe.Label;
        Button_RecipeSource.Text = $"🔗 Source: {recipe.SourceName}";
        Label_RecipeYield.Text = $"{yield} People";
        Label_RecipeYieldLandscape.Text = $"{yield} People";
        Image_RecipeImage.Source = recipe.ImageLink;
        Image_LandscapeRecipeImage.Source = recipe.ImageLink;

        CollectionView_Ingredients.ItemsSource = recipe.Ingredients;

        CollectionView_Nutrition.ItemsSource = manager.NutritionInfo;

        await DisplayTotals();

        ActivityIndicator_Loading.IsRunning = false;
        ActivityIndicator_Loading.IsVisible = false;

        Border_TableView.IsVisible = false;
        Border_TableView.IsVisible = true;
    }

    private async void Button_DecreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
        if (yield == 1)
        {
            return;
        }
        yield--;
        Label_RecipeYield.Text = $"{yield} {((yield > 1) ? "People" : "Person")}";
        Label_RecipeYieldLandscape.Text = $"{yield} {((yield > 1) ? "People" : "Person")}";

        await manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
        await DisplayTotals();
    }

    private async void Button_IncreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
        yield++;
        Label_RecipeYield.Text = $"{yield} People";
        Label_RecipeYieldLandscape.Text = $"{yield} People";

        await manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
        await DisplayTotals();
    }

    private async Task DisplayTotals()
    {
        decimal totalCost = (decimal)manager.CurrentRecipe.TotalCost;
        decimal costPerServe = totalCost / (decimal)manager.CurrentRecipe.Yield;
        Label_TotalCost.Text = totalCost.ToString("C");
        Label_TotalCostLandscape.Text = totalCost.ToString("C");
        Label_CostPerServe.Text = costPerServe.ToString("C");
        Label_CostPerServeLandscape.Text = costPerServe.ToString("C");
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
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private async void Button_SaveRecipe_Pressed(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new();
        string message = "Recipe Saved";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;

        var toast = Toast.Make(message, duration, fontSize);

        try
        {
            await localDBService.AddRecipe(manager.CurrentRecipe);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
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
        Button_DeleteRecipe.IsVisible = wasSaved ? false : true;
    }

    private void Button_Ingredients_Pressed(object sender, EventArgs e)
    {
        ChangeTabColors(true);
        ActivityIndicator_Loading.IsVisible = true;
        ActivityIndicator_Loading.IsRunning = true;
        Border_NutritionTableView.IsVisible = false;
        ActivityIndicator_Loading.IsVisible = false;
        ActivityIndicator_Loading.IsRunning = false;


        Border_TableView.IsVisible = true;
    }

    private async void Button_Nutrition_Pressed(object sender, EventArgs e)
    {
        ChangeTabColors();
        ActivityIndicator_Loading.IsVisible = true;
        ActivityIndicator_Loading.IsRunning = true;
        Border_TableView.IsVisible = false;

        if (manager.NutritionInfo == null)
        {
            try
            {
                manager.NutritionInfo = await manager.GetNutrition();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error:", ex.Message, "Ok");
            }

        }

        CollectionView_Nutrition.ItemsSource = manager.NutritionInfo;
        ActivityIndicator_Loading.IsRunning = false;
        ActivityIndicator_Loading.IsVisible = false;
        Border_NutritionTableView.IsVisible = true;
    }

    private void ChangeTabColors(bool isLeftTab = false)
    {
        Border_Ingredients.Style = (Style)Resources[isLeftTab ? "SelectedTabBorderLocal" : "UnselectedTabBorderLocal"];
        Button_Ingredients.Style = (Style)Resources[isLeftTab ? "SelectedTabButtonLocal" : "UnselectedTabButtonLocal"];

        Border_Nutrition.Style = (Style)Resources[isLeftTab ? "UnselectedTabBorderLocal" : "SelectedTabBorderLocal"];
        Button_Nutrition.Style = (Style)Resources[isLeftTab ? "UnselectedTabButtonLocal" : "SelectedTabButtonLocal"];
    }
}