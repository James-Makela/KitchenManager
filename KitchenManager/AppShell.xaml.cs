using KitchenManager.Controllers;

namespace KitchenManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("recipecard", typeof(RecipeCardPage));
        }
    }
}
