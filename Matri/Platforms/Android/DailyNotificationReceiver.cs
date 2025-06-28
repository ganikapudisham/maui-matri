using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Grpc.Core;

namespace Matri.Platforms.Android;

[BroadcastReceiver(Enabled = true, Exported = true)]
public class DailyNotificationReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        string title = intent.GetStringExtra("title") ?? "🎉 Reminder";
        string message = intent.GetStringExtra("message") ?? "This is your yearly notification.";
        int notificationId = intent.GetIntExtra("notificationId", 0);
        int hour = intent.GetIntExtra("hour", 0);
        int minute = intent.GetIntExtra("minute", 0);

        string channelId = "daily_verse_channel";
        string channelName = "Daily Verse Notifications";

        // Create notification channel if needed
        var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);
        }

        var bitmap = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.notificationicon);


        var builder = new NotificationCompat.Builder(context, channelId)
            .SetSmallIcon(Resource.Drawable.notificationicon)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetLargeIcon(bitmap) // This shows the image to the left of text
            .SetStyle(new NotificationCompat.BigTextStyle().BigText(message)) // Optional: expand text
            .SetAutoCancel(true);

        notificationManager.Notify(notificationId, builder.Build());

        _ = NotificationScheduler.ScheduleDailyNotification(context, hour, minute, notificationId);
    }
}
