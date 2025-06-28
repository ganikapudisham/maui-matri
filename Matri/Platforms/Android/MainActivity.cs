using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CommunityToolkit.Mvvm.Messaging;
using Firebase;
using Matri.Model;
using Matri.Models;
using Matri.Platforms.Android;
using System.Text.Json;
using static Android.Gms.Common.Apis.Api;

namespace Matri;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
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
        if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.PostNotifications) == Permission.Denied)
        {
            ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.PostNotifications }, 1);
        }

        CreateNotificationChannel();

        CreateNewYearNotifications();

        CreateEasterNotifications();

        CreateChristmasNotifications();

        CreateDailyNotifications();
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

    private void CreateChristmasNotifications()
    {
        NotificationScheduler.ScheduleYearlyNotification(
context: this,
day: 25,
month: 12,
hour: 0,
minute: 0,
notificationId: NotificationIdChristmasNight,
title: "🎄 Merry Christmas!",
message: "It's Christmas Night!"
);

        NotificationScheduler.ScheduleYearlyNotification(
context: this,
day: 25,
month: 12,
hour: 9,
minute: 0,
notificationId: NotificationIdChristmasAfternoon, // Use a different ID
title: "🎁 Christmas Morning!",
message: "Good morning! Enjoy your Christmas Day!"
);
    }

    private void CreateNewYearNotifications()
    {
        NotificationScheduler.ScheduleYearlyNotification(
                    context: this,
                    day: 1,
                    month: 1,
                    hour:0,
                    minute:0,
                    notificationId: NotificationIdNewYear,
                    title: "🎆 Happy New Year!",
                    message: "Welcome to a brand new year!"
                );

        NotificationScheduler.ScheduleYearlyNotification(
                    context: this,
                    day: 1,
                    month: 1,
                    hour: 7,
                    minute: 0,
                    notificationId: NotificationIdNewYear,
                    title: "🎆 Happy New Year!",
                    message: "Welcome to a brand new year!"
                );
    }

    private void CreateEasterNotifications()
    {
        NotificationScheduler.ScheduleEasterNotification(
            Android.App.Application.Context,
            DateTime.Now.Year, 0, 0, NotificationIdEaster);

        NotificationScheduler.ScheduleEasterNotification(
            Android.App.Application.Context,
            DateTime.Now.Year, 7, 0, NotificationIdEaster);
    }

    private async Task CreateDailyNotifications()
    {
        var context = Platform.CurrentActivity ?? throw new NullReferenceException("CurrentActivity is null");

        await NotificationScheduler.ScheduleDailyNotification(
    context: context,
    hour: 7,
    minute: 0,
    notificationId: NotificationIdDailyMorning
);

        await NotificationScheduler.ScheduleDailyNotification(
    context: context,
    hour: 20,
    minute: 0,
    notificationId: NotificationIdDailyEvening
);
    }
}
