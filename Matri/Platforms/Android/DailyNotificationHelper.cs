using AndroidX.Work;
using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Android.App.Application;


namespace Matri.Platforms.Android;

public class DailyNotificationHelper
{
    public void ScheduleTodayNotifications()
    {
        // 🔔 Schedule 7:00 AM and 8:00 PM notifications for today
        DateTime sevenAM = DateTime.Today.AddHours(7);
        DateTime eightPM = DateTime.Today.AddHours(20);

        if (DateTime.Now > sevenAM)
            sevenAM = sevenAM.AddDays(1);

        if (DateTime.Now > eightPM)
            eightPM = eightPM.AddDays(1);

        ScheduleNotification("Morning Reminder", "Good morning!", sevenAM);   // 7 AM
        ScheduleNotification("Evening Reminder", "Good evening!", eightPM); // 8 PM

        ScheduleNextMidnightScheduler(DateTime.Today.AddDays(1));
    }

    public void ScheduleNextMidnightScheduler(DateTime scheduleTime)
    {
        //var midnight = DateTime.Today.AddDays(1);
        var delay = (long)(scheduleTime - DateTime.Now).TotalMilliseconds;

        if (delay <= 0)
        {
            // Optional safety check: don't schedule a past time
            return;
        }

        var workManager = WorkManager.GetInstance(Application.Context);

        // Cancel any existing work with the same tag before enqueuing a new one
        workManager.CancelAllWorkByTag("MidnightScheduler");

        var data = new AndroidX.Work.Data.Builder()
            .PutString("title", "Daily Notification Rescheduler")
            .PutString("message", "Scheduling notifications for tomorrow")
            .PutString("tag", "tagDailyScheduler")
            .Build();

        var request = new OneTimeWorkRequest.Builder(typeof(ScheduledNotificationWorker))
            .SetInitialDelay(delay, TimeUnit.Milliseconds)
            .SetInputData(data)
            .AddTag("MidnightScheduler")
            .Build();

        WorkManager.GetInstance(Application.Context).Enqueue(request);
    }

    private void ScheduleNotification(string title, string message, DateTime triggerTime)
    {
        if (triggerTime <= DateTime.Now)
            return;

        var data = new AndroidX.Work.Data.Builder()
            .PutString("title", title)
            .PutString("message", message)
            .PutString("tag", "tagDailyScheduler")
            .Build();

        var request = new OneTimeWorkRequest.Builder(typeof(ScheduledNotificationWorker))
            .SetInitialDelay((long)(triggerTime - DateTime.Now).TotalMilliseconds, TimeUnit.Milliseconds)
            .SetInputData(data)
            .Build();

        WorkManager.GetInstance(Application.Context).Enqueue(request);
    }
}
