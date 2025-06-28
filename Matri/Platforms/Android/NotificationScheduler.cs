using Android.App;
using Android.Content;
using Matri.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Matri.Platforms.Android;

public static class NotificationScheduler
{
    public static void ScheduleYearlyNotification(
        Context context,
        int day,
        int month, // 1–12
        int hour,
        int minute,
        int notificationId,
        string title,
        string message)
    {
        var now = DateTime.Now;
        var target = new DateTime(now.Year, month, day, hour, minute, 0);

        if (target <= now)
        {
            target = target.AddYears(1);
        }

        long triggerTimeMillis = DateTimeToUnixMillis(target);

        var intent = new Intent(context, typeof(YearlyNotificationReceiver));
        intent.PutExtra("title", title);
        intent.PutExtra("message", message);
        intent.PutExtra("notificationId", notificationId);
        intent.PutExtra("day", day);
        intent.PutExtra("month", month);

        var pendingIntent = PendingIntent.GetBroadcast(
            context,
            notificationId,
            intent,
            PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
        );

        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

        alarmManager.SetExactAndAllowWhileIdle(
            AlarmType.RtcWakeup,
            triggerTimeMillis,
            pendingIntent
        );
    }


    public static void ScheduleEasterNotification(Context context, int year, int hour, int minute, int notificationId)
    {
        DateTime easter = GetEasterSunday(year);

        if (easter < DateTime.Today)
        {
            easter = GetEasterSunday(year + 1);
        }

        var intent = new Intent(context, typeof(EasterNotificationReceiver));
        intent.PutExtra("year", easter.Year);
        intent.PutExtra("title", "🐣 Happy Easter!");
        intent.PutExtra("message", $"Wishing you a joyful Easter {easter.Year}!");
        intent.PutExtra("notificationId", notificationId); // Unique per year
        intent.PutExtra("hour", hour);
        intent.PutExtra("minute", minute);

        var pendingIntent = PendingIntent.GetBroadcast(
            context,
            400 + easter.Year,
            intent,
            PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
        );

        var triggerTime = new DateTimeOffset(easter.Year, easter.Month, easter.Day, hour, minute, 0, TimeSpan.Zero).ToUnixTimeMilliseconds();

        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

        alarmManager.SetExactAndAllowWhileIdle(
            AlarmType.RtcWakeup,
            triggerTime,
            pendingIntent
        );
    }

    public static DateTime GetEasterSunday(int year)
    {
        int a = year % 19;
        int b = year / 100;
        int c = year % 100;
        int d = b / 4;
        int e = b % 4;
        int f = (b + 8) / 25;
        int g = (b - f + 1) / 3;
        int h = (19 * a + b - d - g + 15) % 30;
        int i = c / 4;
        int k = c % 4;
        int l = (32 + 2 * e + 2 * i - h - k) % 7;
        int m = (a + 11 * h + 22 * l) / 451;
        int month = (h + l - 7 * m + 114) / 31;
        int day = ((h + l - 7 * m + 114) % 31) + 1;

        return new DateTime(year, month, day);
    }

    public static long DateTimeToUnixMillis(DateTime dt)
    {
        var offset = new DateTimeOffset(dt.ToUniversalTime());
        return offset.ToUnixTimeMilliseconds();
    }

    public static async Task ScheduleDailyNotification(
    Context context,
    int hour,
    int minute,
    int notificationId)
    {
        try
        {
            HttpClient client = new();
            string url = "https://labs.bible.org/api/?passage=random&type=json";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            // Parse JSON into array of BibleVerse
            var verses = JsonSerializer.Deserialize<BibleVerse[]>(json);

            if (verses?.Any() == true)
            {
                var verse = verses[0];

                var intent = new Intent(context, typeof(DailyNotificationReceiver));
                intent.PutExtra("title", $"{verse.bookname} {verse.chapter}:{verse.verse}");
                intent.PutExtra("message", $"{verse.text}");
                intent.PutExtra("notificationId", notificationId);
                intent.PutExtra("minute", minute);
                intent.PutExtra("hour", hour);

                var pendingIntent = PendingIntent.GetBroadcast(
                    context,
                    notificationId,
                    intent,
                    PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
                );

                // Calculate the next trigger time using DateTime
                var now = DateTime.Now;
                var target = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);

                if (target <= now)
                {
                    target = target.AddDays(1);
                }

                // Convert DateTime to milliseconds since epoch
                var utcOffset = TimeZoneInfo.Local.GetUtcOffset(target);
                long triggerTimeMillis = (long)(target.ToUniversalTime() - DateTime.UnixEpoch).TotalMilliseconds;

                var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

                alarmManager.SetExactAndAllowWhileIdle(
                    AlarmType.RtcWakeup,
                    triggerTimeMillis,
                    pendingIntent
                );
            }
        }
        catch (Exception ex)
        {
        }
    }
}
