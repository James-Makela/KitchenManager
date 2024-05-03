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
    }
}
