using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Platforms.Android;

[BroadcastReceiver(Enabled = true, Exported = true)]
public class EasterNotificationReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        string title = intent.GetStringExtra("title") ?? "🐣 Happy Easter!";
        string message = intent.GetStringExtra("message") ?? "Wishing you a joyful Easter!";
        int notificationId = intent.GetIntExtra("notificationId", 0);
        int currentYear = intent.GetIntExtra("year", DateTime.Now.Year);
        int hour = intent.GetIntExtra("hour", 0);
        int minute = intent.GetIntExtra("minute", 0);

        string channelId = "yearly_easter_channel";
        string channelName = "Yearly Easter Notifications";

        // Create notification channel if needed
        var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);
        }

        var bitmap = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.notificationicon);

        // Show the notification
        var builder = new NotificationCompat.Builder(context, channelId)
            .SetSmallIcon(Resource.Drawable.notificationicon)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetLargeIcon(bitmap) // This shows the image to the left of text
            .SetStyle(new NotificationCompat.BigTextStyle().BigText(message)) // Optional: expand text
            .SetAutoCancel(true);

        NotificationManagerCompat.From(context).Notify(notificationId, builder.Build());

        // 🗓️ Schedule Easter for next year
        NotificationScheduler.ScheduleEasterNotification(context, currentYear + 1, hour, minute, notificationId);
    }
}
