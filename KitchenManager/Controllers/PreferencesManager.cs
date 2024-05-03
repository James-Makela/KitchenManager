using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenManager.Controllers
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
        }
    }
}
