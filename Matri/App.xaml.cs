﻿using Matri.ViewModel;
using Plugin.Maui.ScreenSecurity;

namespace Matri;

public partial class App : Application
{
    public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
    public App(AppShellViewModel viewModel)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5eeXVWRmZdVkdyWUM=");
        InitializeComponent();
        MainPage = new AppShell(viewModel);

#if ANDROID
        // Schedule daily notifications at 7 AM and 8 PM
        var scheduler = new Matri.Platforms.Android.DailyNotificationHelper();
        scheduler.ScheduleTodayNotifications();

        // Christmas

        var schedulerDate = new Matri.Platforms.Android.DateNotificationScheduler();

        var christmas = new DateTime(DateTime.Now.Year, 12, 25, 9, 0, 0);

        if (christmas < DateTime.Now)
        {
            christmas = christmas.AddYears(1);
        }

        schedulerDate.ScheduleNotification(christmas, "Merry Christmas!", "Rejoice! Christ is born 🎄", "tagChristmas");

        // end of Christmas

        // Easter
        var easter = VersionHelper.GetEasterDate(DateTime.Now.Year);

        if (easter < DateTime.Now)
        {
            easter = VersionHelper.GetEasterDate(DateTime.Now.Year + 1);
        }

        schedulerDate.ScheduleNotification(easter, "He is Risen!", "Celebrate the resurrection of Jesus ✝️","tagEaster");

        // end of easter

        //New Year

        var newYear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);

        if (newYear < DateTime.Now)
        {
            newYear = newYear.AddYears(1);
        }

        schedulerDate.ScheduleNotification(newYear, "He is Risen!", "Celebrate the resurrection of Jesus ✝️", "tagEaster");

        // end of New Year
#endif
    }

    
}
