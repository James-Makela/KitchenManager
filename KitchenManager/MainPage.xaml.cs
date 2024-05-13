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
            await Shell.Current.GoToAsync("//recipes", true);
        }

        private async void Button_Inventory_Pressed(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//inventory", true);
        }

        private async void Button_CookNow_Pressed(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//cooknow");
        }

        private async void Button_Settings_Pressed(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//preferences", true);
        }
    }

}
