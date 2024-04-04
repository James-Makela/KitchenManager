using KitchenManager.Models;
using KitchenManager.Controllers;

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
		Label_RecipeSourceName.Text = recipe.SourceName;
		Label_RecipeYield.Text = $"Yield: {yield}";
		ListView_Ingredients.ItemsSource = null;
		ListView_Ingredients.ItemsSource = recipe.Ingredients;
		Image_RecipeImage.Source = recipe.ImageLink;
    }

    private void Button_DecreaseYield_Pressed(object sender, EventArgs e)
    {
		int.TryParse(Label_RecipeYield.Text.Split(":")[1], out int yield);
		if (yield == 1)
		{
			return;
		}
		yield--;
        Label_RecipeYield.Text = $"Yield: {yield}";

        manager.ChangeYield(yield);

        ListView_Ingredients.ItemsSource = null;
        ListView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
    }

    private void Button_IncreaseYield_Pressed(object sender, EventArgs e)
    {
        int.TryParse(Label_RecipeYield.Text.Split(":")[1], out int yield);
        yield++;
		Label_RecipeYield.Text = $"Yield: {yield}";

        manager.ChangeYield(yield);

        ListView_Ingredients.ItemsSource = null;
        ListView_Ingredients.ItemsSource = manager.CurrentRecipe.Ingredients;
    }
}