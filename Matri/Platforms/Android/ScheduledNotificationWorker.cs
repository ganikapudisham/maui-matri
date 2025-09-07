//using Android.App;
//using Android.Content;
//using Android.OS;
//using AndroidX.Core.App;
//using AndroidX.Work;
//using Matri.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using static Android.Icu.Text.CaseMap;

//namespace Matri.Platforms.Android;

//public class ScheduledNotificationWorker : Worker
//{
//    public ScheduledNotificationWorker(Context context, WorkerParameters workerParams)
//        : base(context, workerParams) { }

//    public override Result DoWork()
//    {
//        var tag = InputData.GetString("tag");

//        if (tag == "tagDailyScheduler")
//        {
//            // Show Bible verse
//            Task.Run(async () => await ShowBibleVerseNotificationAsync(ApplicationContext)).Wait();

//            // Reschedule next notifications
//            var scheduler = new DailyNotificationHelper();
//            scheduler.ScheduleTodayNotifications();
//        }
//        else if (tag == "tagEaster")
//        {
//            var title = InputData.GetString("title");
//            var message = InputData.GetString("message");

//            var nextEaster = VersionHelper.GetEasterDate(DateTime.Now.Year + 1);
//            var nextEasterTime = new DateTime(nextEaster.Year, nextEaster.Month, nextEaster.Day, 0, 0, 0); // 9 AM

//            var scheduler = new DateNotificationScheduler();
//            scheduler.ScheduleNotification(nextEasterTime, title, message, tag);
//        }
//        else //Birthday, New Year, Christmas
//        {
//            // Show custom message
//            var title = InputData.GetString("title");
//            var message = InputData.GetString("message");
//            var dateStr = InputData.GetString("date");

//            if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out var originalDate))
//            {
//                var nextYearDate = new DateTime(
//                    DateTime.Now.Year + 1,
//                    originalDate.Month,
//                    originalDate.Day,
//                    originalDate.Hour,
//                    originalDate.Minute,
//                    originalDate.Second
//                );

//                if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(message))
//                {
//                    ShowCustomNotification(title, message);
//                }

//                var nextYearEventTime = new DateTime(nextYearDate.Year, nextYearDate.Month, nextYearDate.Day, 0, 0, 0);
//                var scheduler = new DateNotificationScheduler();
//                scheduler.ScheduleNotification(nextYearDate, title, message, tag);
//            }
//        }

//        return Result.InvokeSuccess();
//    }


//    private async Task ShowBibleVerseNotificationAsync(Context context)
//    {
//        try
//        {
//            HttpClient client = new();
//            string url = "https://labs.bible.org/api/?passage=random&type=json";

//            var request = new HttpRequestMessage(HttpMethod.Get, url);
//            request.Headers.Add("User-Agent", "Mozilla/5.0");

//            var response = await client.SendAsync(request);
//            response.EnsureSuccessStatusCode();

//            string json = await response.Content.ReadAsStringAsync();
//            var verses = JsonSerializer.Deserialize<BibleVerse[]>(json);

//            if (verses?.Any() == true)
//            {
//                var verse = verses[0];

//                // Build notification
//                var channelId = "bible_channelV2";
//                var notificationManager = NotificationManagerCompat.From(context);

//                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
//                {
//                    var channel = new NotificationChannel(
//        channelId,
//        "Bible Verses",
//        NotificationImportance.High // 👈 This is critical
//    );
//                    channel.EnableVibration(true);
//                    channel.EnableLights(true);
//                    notificationManager.CreateNotificationChannel(channel);
//                }

//                string title = $"{verse.bookname} {verse.chapter}:{verse.verse}";
//                string message = verse.text;

//                // 👉 Create intent to open app and show popup
//                Intent intent = new(context, typeof(MainActivity));
//                intent.PutExtra("title", title);
//                intent.PutExtra("message", message);
//                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

//                var pendingIntent = PendingIntent.GetActivity(
//                    context,
//                    0,
//                    intent,
//                    PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
//                );

//                var notification = new NotificationCompat.Builder(context, channelId)
//                    .SetContentTitle(title)
//                    .SetContentText(verse.text)
//                    .SetSmallIcon(Resource.Drawable.notificationicon)
//                    .SetStyle(new NotificationCompat.BigTextStyle().BigText(verse.text))
//                    .SetPriority((int)NotificationPriority.High)
//                    .SetDefaults((int)NotificationDefaults.All)
//                    .SetAutoCancel(true)
//                    .SetContentIntent(pendingIntent) // 👈 Add this line
//                    .Build();

//                notificationManager.Notify(new Random().Next(), notification);
//            }
//        }
//        catch (Exception ex)
//        {
//            System.Diagnostics.Debug.WriteLine("Notification error: " + ex.Message);
//        }
//    }

//    private void ShowCustomNotification(string title, string message)
//    {
//        var context = ApplicationContext;
//        var channelId = "bible_channelV2";
//        var notificationManager = NotificationManagerCompat.From(context);

//        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
//        {
//            var channel = new NotificationChannel(
//                channelId,
//                "Bible Verses",
//                NotificationImportance.High);
//            channel.EnableVibration(true);
//            channel.EnableLights(true);
//            notificationManager.CreateNotificationChannel(channel);
//        }

//        Intent intent = new(context, typeof(MainActivity));
//        intent.PutExtra("title", title);
//        intent.PutExtra("message", message);
//        intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

//        var pendingIntent = PendingIntent.GetActivity(
//            context,
//            0,
//            intent,
//            PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
//        );

//        var notification = new NotificationCompat.Builder(context, channelId)
//            .SetContentTitle(title)
//            .SetContentText(message)
//            .SetSmallIcon(Resource.Drawable.notificationicon)
//            .SetStyle(new NotificationCompat.BigTextStyle().BigText(message))
//            .SetPriority((int)NotificationPriority.High)
//            .SetDefaults((int)NotificationDefaults.All)
//            .SetAutoCancel(true)
//            .SetContentIntent(pendingIntent)
//            .Build();

//        notificationManager.Notify(new Random().Next(), notification);
//    }

//}

