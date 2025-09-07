using Matri.ViewModel;
using Plugin.Maui.ScreenSecurity;

namespace Matri;

public partial class App : Application
{
    public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
    public App(AppShellViewModel viewModel)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXZec3VVQmJZU0NzXUJWYEk=");
        InitializeComponent();
        MainPage = new AppShell(viewModel);

#if ANDROID
        // Schedule daily notifications at 7 AM and 8 PM
        //var scheduler = new Matri.Platforms.Android.DailyNotificationHelper();
        //scheduler.ScheduleTodayNotifications();

        // Christmas

        //var schedulerDate = new Matri.Platforms.Android.DateNotificationScheduler();

        //var christmas = new DateTime(DateTime.Now.Year, 12, 25, 9, 0, 0);

        //if (christmas < DateTime.Now)
        //{
        //    christmas = christmas.AddYears(1);
        //}

        //schedulerDate.ScheduleNotification(christmas, "Merry Christmas!", "Rejoice! Christ is born 🎄", "tagChristmas");

        // end of Christmas

        // Easter
        //var easter = VersionHelper.GetEasterDate(DateTime.Now.Year);

        //if (easter < DateTime.Now)
        //{
        //    easter = VersionHelper.GetEasterDate(DateTime.Now.Year + 1);
        //}

        //schedulerDate.ScheduleNotification(easter, "He is Risen!", "Celebrate the resurrection of Jesus ✝️","tagEaster");

        // end of easter

        //New Year

        //var newYear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);

        //if (newYear < DateTime.Now)
        //{
        //    newYear = newYear.AddYears(1);
        //}

        //schedulerDate.ScheduleNotification(newYear, "He is Risen!", "Celebrate the resurrection of Jesus ✝️", "tagEaster");

        // end of New Year
#endif
    }

    protected override void OnAppLinkRequestReceived(Uri uri)
    {
        base.OnAppLinkRequestReceived(uri);

        if (uri != null)
        {
            // Example: christianjodi://payment-success
            if (uri.Host == "payment-success")
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync("///LoginPage");
                });
            }
            else if (uri.Host == "payment-failed")
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync("///LoginPage");
                });
            }
        }
    }

}
