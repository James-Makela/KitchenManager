using KitchenManager;

namespace KitchenManager
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Recipes_Clicked(object sender, EventArgs e)
        {
            // Button_Recipes.BackgroundColor = Color.Parse("#415a77");
            RecipesPage recipiesPage = new RecipesPage();
            await Navigation.PushModalAsync(recipiesPage);
            // Button_Recipes.BackgroundColor = Color.Parse("#778DA9");
        }
    }

}
