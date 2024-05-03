using KitchenManager;

namespace KitchenManager
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void Button_Recipes_Clicked(object sender, EventArgs e)
        {
            Button_Recipes.BackgroundColor = Color.Parse("#415a77");
            await Shell.Current.GoToAsync("//recipes", true);
            Button_Recipes.BackgroundColor = Color.Parse("#778DA9");
        }

        private async void Button_Inventory_Pressed(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//inventory", true);
        }
    }

}
