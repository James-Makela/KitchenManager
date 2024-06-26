﻿namespace KitchenManager.Controllers
{
    internal static class PreferencesManager
    {

        public static bool GetUnit()
        {
            return Preferences.Default.Get<bool>("units", true);
        }

        public static void SetUnit(bool value)
        {
            Preferences.Default.Set<bool>("units", value);
        }

        public static int GetPeople()
        {
            return Preferences.Default.Get<int>("people", 4);
        }

        public static void SetPeople(int value)
        {
            Preferences.Default.Set<int>("people", value);
        }

        public static int GetTheme()
        {
            return Preferences.Default.Get<int>("theme", 0);
        }

        public static void SetTheme(int value)
        {
            Preferences.Default.Set<int>("theme", value);
            Preferences.Default.Set<bool>("themeSetByUser", true);
        }

        public static bool CheckThemeSet()
        {
            return Preferences.Default.Get<bool>("themeSetByUser", false);
        }

        public static void ApplyTheme()
        {
            int theme = Preferences.Get("theme", 0);
            switch (theme)
            {
                case 0:
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;

                case 1:
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }
}
