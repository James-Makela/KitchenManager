using KitchenManager.Controllers;

namespace KitchenManager
{
    public partial class App : Application
    {

        public RecipeManager recipeManager = new();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
