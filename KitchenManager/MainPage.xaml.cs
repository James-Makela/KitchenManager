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
            RecipesPage recipiesPage = new RecipesPage();
            await Navigation.PushModalAsync(recipiesPage);
        }
    }

}
