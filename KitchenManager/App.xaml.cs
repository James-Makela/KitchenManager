using KitchenManager.Controllers;
using KitchenManager.Themes;

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

            PreferencesManager.ApplyTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            Resources.MergedDictionaries.Clear();
            if (PreferencesManager.CheckThemeSet())
            {
                PreferencesManager.ApplyTheme();
            }
            switch (e.RequestedTheme)
            {
                case AppTheme.Light:
                    Resources.MergedDictionaries.Add(new DefaultTheme());
                    break;
                case AppTheme.Dark:
                    Resources.MergedDictionaries.Add(new DarkTheme());
                    break;
            }
        }
    }
}
