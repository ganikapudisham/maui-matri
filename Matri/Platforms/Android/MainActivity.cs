using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Models;
using Matri.Views;

namespace Matri;

[Activity(LaunchMode = LaunchMode.SingleTop, 
    Theme = "@style/Maui.SplashTheme", 
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

[IntentFilter(
    new[] { Android.Content.Intent.ActionView },
    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
    DataScheme = "christianjodi")]
public class MainActivity : MauiAppCompatActivity
{
    internal static readonly string NotificationChannelCJ = "ChristianJodiNC";
    internal static readonly int NotificationIdCJ = 101;

    const int NotificationIdChristmasNight = 301;
    const int NotificationIdChristmasAfternoon = 302;
    const int NotificationIdEaster = 303;
    const int NotificationIdNewYear = 304;
    const int NotificationIdDailyMorning = 305;
    const int NotificationIdDailyEvening = 306;
    const int NotificationIdTest = 307;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        HandleIntent(Intent);

        if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.PostNotifications) == Permission.Denied)
        {
            ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.PostNotifications }, 1);
        }

        CreateNotificationChannel();

    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        if (intent.Extras != null)
        {
            foreach (var key in intent.Extras.KeySet())
            {
                if (key == "NotificationFrom")
                {
                    string notificationFromId = intent.Extras.GetString(key);
                    if (Preferences.ContainsKey("NotificationFrom"))
                        Preferences.Remove("NotificationFrom");

                    Preferences.Set("NotificationFrom", notificationFromId);

                    WeakReferenceMessenger.Default.Send(new PushNotificationReceived("test"));
                }
            }
        }

        // 2. Handle app link / deep link
        var data = intent.Data;
        if (data != null && data.Scheme == "christianjodi")
        {
            Microsoft.Maui.Controls.Application.Current?.SendOnAppLinkRequestReceived(new Uri(data.ToString()));
        }

        // Optional: If you have additional logic
        HandleIntent(intent);
    }

    private void CreateNotificationChannel()
    {
        if (OperatingSystem.IsOSPlatformVersionAtLeast("android", 26))
        {
            var channel = new NotificationChannel(NotificationChannelCJ, NotificationIdCJ.ToString(), NotificationImportance.Default);

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }

    private void HandleIntent(Intent? intent)
    {
        if (intent?.HasExtra("message") == true && intent?.HasExtra("title") == true)
        {
            string message = intent.GetStringExtra("message");
            string title = intent.GetStringExtra("title");

            Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
            {
                var popup = new VersePopup(title, message);
                Microsoft.Maui.Controls.Application.Current.MainPage?.ShowPopup(popup);
            });
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Intent does not contain expected extras");
        }
    }
}
