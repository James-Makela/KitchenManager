using Android.App;
using Android.Content.PM;
using Android.OS;

namespace KitchenManager
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#1b263b"));
            Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#1b263b"));

            base.OnCreate(savedInstanceState);
        }
    }
}
