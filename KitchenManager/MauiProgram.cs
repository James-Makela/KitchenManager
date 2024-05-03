using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace KitchenManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Removes underline from entry in android.
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Placeholder", (handler, view) =>
            {
#if ANDROID
                // Entry
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            // Remove underline from picker
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
#if ANDROID
                // Picker
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping("MyCustomizationSearchBar", (handler, view) =>
            {
#if ANDROID

                Android.Widget.LinearLayout linearLayout = handler.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;
                linearLayout.Background = null;

#endif
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
