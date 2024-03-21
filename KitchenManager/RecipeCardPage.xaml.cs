using KitchenManager.Models;

namespace KitchenManager;

public partial class RecipeCardPage : ContentPage
{
	public RecipeCardPage(Recipe recipe)
	{
		InitializeComponent();
		PopulateFields(recipe);
	}

    void PopulateFields(Recipe recipe)
    {
		Label_RecipeLabel.Text = recipe.Label;
		Label_RecipeSourceName.Text = recipe.SourceName;
		Label_RecipeSourceURL.Text = recipe.SourceLink;
		Label_RecipeYield.Text = $"Yield: {recipe.Yield}";
		ListView_Ingredients.ItemsSource = recipe.Ingredients;
		Image_RecipeImage.Source = recipe.ImageLink;
		Label_RecipeID.Text = recipe.URI;
    }
}