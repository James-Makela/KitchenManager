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

            PreferencesManager.ApplyTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            if (PreferencesManager.CheckThemeSet())
            {
                PreferencesManager.ApplyTheme();
            }
            switch (e.RequestedTheme)
            {
                case AppTheme.Light:
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;
                case AppTheme.Dark:
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }
}
