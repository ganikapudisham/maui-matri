using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business.Impl;
using Matri.Data.Impl;
using Matri.Model;
using Matri.Models;
using Matri.Views;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Crashlytics;
using System.Text;
using System.Text.Json;

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

    private const string DefaultChannelId = "default_channel";
    private const string DefaultChannelName = "General";

    protected override async void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        HandleIntent(Intent);

        if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.PostNotifications) == Permission.Denied)
        {
            ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.PostNotifications }, 1);
        }

        CreateNotificationChannel();
        try
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();

            var fcm = Plugin.Firebase.CloudMessaging.CrossFirebaseCloudMessaging.Current;

            CrossFirebaseCloudMessaging.Current.NotificationReceived += (s, e) =>
            {
                string title = e.Notification?.Title ?? "No title";
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    //NotificationHelper.ShowCustomNotification(title, "");
                });
            };

            CrossFirebaseCloudMessaging.Current.TokenChanged += (s, e) =>
            {
                //SendTokenToBackendAsync(e.Token);
                GetOrCreateDeviceId();
                if (Preferences.ContainsKey("FcmToken"))
                {
                    Preferences.Remove("FcmToken");
                }
                Preferences.Set("FcmToken", e.Token);
            };

            var token = await fcm.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                //SendTokenToBackendAsync(token);
                GetOrCreateDeviceId();
                if (Preferences.ContainsKey("FcmToken"))
                {
                    Preferences.Remove("FcmToken");
                }
                Preferences.Set("FcmToken", token);
            }

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    CrossFirebaseCrashlytics.Current.RecordException(ex);
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                CrossFirebaseCrashlytics.Current.RecordException(e.Exception);
                e.SetObserved();
            };
        }
        catch (Exception ex)
        {
            CrossFirebaseCrashlytics.Current.RecordException(ex);
        }
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

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelDefault = new NotificationChannel(
                DefaultChannelId,
                DefaultChannelName,
                NotificationImportance.Default)
            {
                Description = "General notifications"
            };

            var manager = (NotificationManager)GetSystemService(NotificationService);
            manager.CreateNotificationChannel(channelDefault);
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
                //Microsoft.Maui.Controls.Application.Current.MainPage?.ShowPopup(popup);
            });
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Intent does not contain expected extras");
        }
    }

    //private static async Task SendTokenToBackendAsync(string FcmToken)
    //{
    //    var serviceRepository = new ServiceRepository();
    //    // ^ Replace with your actual implementation class

    //    // 2. Create ServiceManager instance with dependency injected
    //    var serviceManager = new ServiceManager(serviceRepository);

    //    try
    //    {
    //        var deviceId = GetOrCreateDeviceId();

    //        var fcmToken = new FCMToken
    //        {
    //            DeviceId = deviceId,
    //            FcmToken = FcmToken
    //        };

    //        //var json = JsonSerializer.Serialize(payload);
    //        //var content = new StringContent(json, Encoding.UTF8, "application/json");

    //        //using var httpClient = new HttpClient();
    //        //var url = $"https://api.christianjodi.com/devices/register-fcm-token";
    //        var registered = await serviceManager.CreateUpdateDeviceToken("",fcmToken);
    //        //var response = await httpClient.PostAsync(url, content);

    //        //if (response.IsSuccessStatusCode)
    //        //{
    //        //    Console.WriteLine("✅ FCM token registered with backend.");
    //        //}
    //        //else
    //        //{
    //        //    Console.WriteLine($"❌ Failed to register token: {response.StatusCode}");
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"❌ Exception in SendTokenToBackendAsync: {ex.Message}");
    //        CrossFirebaseCrashlytics.Current.RecordException(ex);
    //    }
    //}

    private static string GetOrCreateDeviceId()
    {
        const string key = "DeviceId";
        var id = Preferences.Get(key, null);

        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
            Preferences.Set(key, id);
        }

        return id;
    }
}
