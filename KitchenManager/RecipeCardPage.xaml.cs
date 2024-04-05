using KitchenManager.Models;
using KitchenManager.Controllers;
using System.Collections.ObjectModel;

namespace KitchenManager;

public partial class RecipeCardPage : ContentPage
{
	RecipeManager manager = new();
	public RecipeCardPage(Recipe recipe)
	{
		manager.CurrentRecipe = recipe;
		InitializeComponent();
		PopulateFields(manager.CurrentRecipe);
	}

    void PopulateFields(Recipe recipe)
    {
		int yield = Convert.ToInt32(recipe.Yield);

		Label_RecipeLabel.Text = recipe.Label;
		Button_RecipeSource.Text = $"🔗 Source: {recipe.SourceName}";
		Label_RecipeYield.Text = $"{yield} People";
		CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
		Image_RecipeImage.Source = recipe.ImageLink;
    }

    private void Button_DecreaseYield_Pressed(object sender, EventArgs e)
    {
		int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
		if (yield == 1)
		{
			return;
		}
		yield--;
        Label_RecipeYield.Text = $"{yield} People";

        manager.ChangeYield(yield);

        CollectionView_Ingredients.ItemsSource = null;
        CollectionView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
    }

    private void Button_IncreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(" ")[0], out int yield);
        yield++;
		Label_RecipeYield.Text = $"{yield} People";

        manager.ChangeYield(yield);

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
}