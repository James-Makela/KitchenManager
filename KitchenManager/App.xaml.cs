using KitchenManager.Controllers;

namespace KitchenManager
{
    public partial class App : Application
    {

        public RecipeManager recipeManager = new();
        public LocalDBService localDBService = new();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
