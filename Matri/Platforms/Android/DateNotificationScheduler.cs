using AndroidX.Work;
using Java.Util.Concurrent;
using Android.App;
using Application = Android.App.Application;
using Matri.Abstract;


[assembly: Dependency(typeof(Matri.Platforms.Android.DateNotificationScheduler))]

namespace Matri.Platforms.Android;

public class DateNotificationScheduler : IDateNotificationScheduler
{
    public void CancelNotification(string tag)
    {
        WorkManager.GetInstance(global::Android.App.Application.Context)
                        .CancelAllWorkByTag(tag);
    }

    public void ScheduleNotification(DateTime dateTime, string title, string message, string tag)
    {
        var delay = (long)(dateTime - DateTime.Now).TotalMilliseconds;
        if (delay <= 0) return;

        var data = new AndroidX.Work.Data.Builder()
            .PutString("title", title)
            .PutString("message", message)
            .PutString("date",dateTime.ToString())
            .Build();

        var request = new OneTimeWorkRequest.Builder(typeof(ScheduledNotificationWorker))
            .SetInitialDelay(delay, TimeUnit.Milliseconds)
            .SetInputData(data)
            .AddTag(tag) // ✅ This is essential
            .Build();

        WorkManager.GetInstance(Application.Context).Enqueue(request);
    }

}
