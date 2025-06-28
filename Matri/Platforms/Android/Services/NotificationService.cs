using Android.App;
using Matri.Platforms.Android.Services;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Text.Format;
using static Android.Gms.Common.Apis.Api;
using Matri.Model;
using System.Text.Json;

[assembly: Dependency(typeof(NotificationService))]
namespace Matri.Platforms.Android.Services;

public class NotificationService : Abstract.INotificationService
{
    private static readonly HttpClient client = new();

    public void ScheduleBirthdayNotification(int day, int month, int notificationId, string title, string message)
    {
        var context = Platform.CurrentActivity ?? throw new NullReferenceException("CurrentActivity is null");

        NotificationScheduler.ScheduleYearlyNotification(
            context: context,
            day: day,
            month: month,
            hour: 0,
            minute: 0,
            notificationId: notificationId,
            title: title,
            message: message
        );
    }

    public void CancelBirthdayNotification(int notificationId)
    {
        var context = Microsoft.Maui.MauiApplication.Current.ApplicationContext;

        var intent = new Intent(context, typeof(YearlyNotificationReceiver));

        var pendingIntent = PendingIntent.GetBroadcast(
            context,
            notificationId,
            intent,
            PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
        );

        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
        alarmManager.Cancel(pendingIntent);
    }
}
