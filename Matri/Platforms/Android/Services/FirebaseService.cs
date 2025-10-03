using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace Matri.Platforms.Services;

[Service(Exported = true)]
[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
public class MyFirebaseMessagingService : FirebaseMessagingService
{
    public MyFirebaseMessagingService()
    {

    }
    //public override void OnNewToken(string token)
    //{
    //    base.OnNewToken(token);
    //    if (Preferences.ContainsKey("DeviceToken"))
    //    {
    //        Preferences.Remove("DeviceToken");
    //    }
    //    Preferences.Set("DeviceToken", token);
    //    //SendTokenToServer(token);
    //}

    public override void OnMessageReceived(RemoteMessage message)
    {
        base.OnMessageReceived(message);

        var notification = message.GetNotification();

        ShowNotification(notification.Body, notification.Title, message.Data);
    }

    private void ShowNotification(string messageBody, string title, IDictionary<string, string> data)
    {

        var intent = new Intent(this, typeof(Matri.MainActivity));
        intent.AddFlags(ActivityFlags.ClearTop);
        intent.AddFlags(ActivityFlags.SingleTop);

        foreach (var key in data.Keys)
        {
            string value = data[key];
            intent.PutExtra(key, value);
        }

        var pendingIntent = PendingIntent.GetActivity(this,
            MainActivity.NotificationIdCJ, intent, PendingIntentFlags.OneShot | PendingIntentFlags.Immutable);

        var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.NotificationChannelCJ)
            .SetContentTitle(title)
            .SetSmallIcon(Resource.Mipmap.appicon)
            .SetContentText(messageBody)
            .SetChannelId(MainActivity.NotificationChannelCJ)
            .SetContentIntent(pendingIntent)
            .SetAutoCancel(true)
            .SetPriority((int)NotificationPriority.Max);

        var notificationManager = NotificationManagerCompat.From(this);
        notificationManager.Notify(MainActivity.NotificationIdCJ, notificationBuilder.Build());
    }
}
