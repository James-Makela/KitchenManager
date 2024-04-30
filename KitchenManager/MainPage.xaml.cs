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
            Button_Recipes.BackgroundColor = Color.Parse("#415a77");
            RecipesPage recipesPage = new RecipesPage();
            await Navigation.PushModalAsync(recipesPage);
            Button_Recipes.BackgroundColor = Color.Parse("#778DA9");
        }

        private async void Button_Inventory_Pressed(object sender, EventArgs e)
        {
            InventoryPage inventoryPage = new InventoryPage();
            await Navigation.PushModalAsync(inventoryPage);
        }
    }

}
