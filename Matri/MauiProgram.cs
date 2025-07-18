﻿using CommunityToolkit.Maui;
using Matri.ViewModel;
using Matri.Business;
using Matri.Business.Impl;
using Matri.Data;
using Matri.Data.Impl;
using Matri.Views;
using Matri.Views.MyAccount;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Matri.Services;
using Matri.Helper;
using Matri.Abstract;
using Microsoft.Maui.LifecycleEvents;
using Matri.Views.Popups;
using Plugin.Maui.ScreenSecurity;

#if ANDROID
using Matri.Platforms.Android;
#endif

namespace Matri;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .RegisterFirebase()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                fonts.AddFont("fa-v4compatibility.ttf", "fontAwsCompatibility");
                //fonts.AddFont("FontAwesome5Solid.otf", "FontAwesomeSolid");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");

                fonts.AddFont("Montserrat-Bold.ttf", "Montserrat-Bold");
                fonts.AddFont("Montserrat-Medium.ttf", "Montserrat-Medium");
                fonts.AddFont("Montserrat-Regular.ttf", "Montserrat-Regular");
                //fonts.AddFont("Montserrat-Semibold.ttf", "Montserrat-Semibold");
            })
            .ConfigureEssentials(essentials => 
            { 
                essentials.UseVersionTracking(); 
            });

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();
        //https://github.com/andreinitescu/IconFont2Code
        //https://andreinitescu.github.io/IconFont2Code/
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<RegisterPage>();

        builder.Services.AddSingleton<ForgotPasswordViewModel>();
        builder.Services.AddSingleton<ForgotPasswordPage>();

        builder.Services.AddSingleton<AllProfilesViewModel>();
        builder.Services.AddSingleton<AllProfilesPage>();

        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddSingleton<ProfileDetailsViewModel>();
        builder.Services.AddSingleton<ProfileDetailsPage>();

        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<SettingsPage>();

        builder.Services.AddSingleton<IServiceManager, ServiceManager>();
        builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();

        builder.Services.AddSingleton<SearchViewModel>();
        builder.Services.AddSingleton<SearchPage>();

        builder.Services.AddSingleton<SearchByIdViewModel>();
        builder.Services.AddSingleton<SearchByIdPage>();

        builder.Services.AddSingleton<SearchAdvancedViewModel>();
        builder.Services.AddSingleton<SearchAdvancedPage>();

        builder.Services.AddSingleton<SearchResultsViewModel>();
        builder.Services.AddSingleton<SearchResultsPage>();

        builder.Services.AddSingleton<MyAccountViewModel>();
        builder.Services.AddSingleton<MyAccountPage>();

        builder.Services.AddSingleton<EditProfileViewModel>();
        builder.Services.AddSingleton<EditProfilePage>();

        builder.Services.AddSingleton<EditBasicViewModel>();
        builder.Services.AddSingleton<EditBasicPage>();

        builder.Services.AddSingleton<EditAcademicsViewModel>();
        builder.Services.AddSingleton<EditAcademicsPage>();

        builder.Services.AddSingleton<EditContactsViewModel>();
        builder.Services.AddSingleton<EditContactsPage>();

        builder.Services.AddSingleton<EditExpectationsViewModel>();
        builder.Services.AddSingleton<EditExpectationsPage>();

        builder.Services.AddSingleton<EditFamilyViewModel>();
        builder.Services.AddSingleton<EditFamilyPage>();

        builder.Services.AddSingleton<EditHoroscopeViewModel>();
        builder.Services.AddSingleton<EditHoroscopePage>();

        builder.Services.AddSingleton<EditLifeStyleViewModel>();
        builder.Services.AddSingleton<EditLifeStylePage>();

        builder.Services.AddSingleton<EditPhotoViewModel>();
        builder.Services.AddSingleton<EditPhotoPage>();

        builder.Services.AddSingleton<EditPhysicalViewModel>();
        builder.Services.AddSingleton<EditPhysicalPage>();

        builder.Services.AddSingleton<EditReligionViewModel>();
        builder.Services.AddSingleton<EditReligionPage>();

        builder.Services.AddSingleton<ChangePasswordViewModel>();
        builder.Services.AddSingleton<ChangePasswordPage>();

        builder.Services.AddSingleton<BlockedProfilesViewModel>();
        builder.Services.AddSingleton<BlockedProfilesPage>();

        builder.Services.AddSingleton<LikedProfilesViewModel>();
        builder.Services.AddSingleton<LikedProfilesPage>();

        builder.Services.AddSingleton<NewProfilesViewModel>();
        builder.Services.AddSingleton<NewProfilesPage>();

        builder.Services.AddSingleton<VisitorsViewModel>();
        builder.Services.AddSingleton<VisitorsPage>();

        builder.Services.AddSingleton<RequestsReceivedViewModel>();
        builder.Services.AddSingleton<RequestsReceivedPage>();

        builder.Services.AddSingleton<RequestsSentViewModel>();
        builder.Services.AddSingleton<RequestsSentPage>();

        builder.Services.AddSingleton<AboutUsViewModel>();
        builder.Services.AddSingleton<AboutUsPage>();

        builder.Services.AddSingleton<ContactUsViewModel>();
        builder.Services.AddSingleton<ContactUsPage>();

        builder.Services.AddSingleton<NotificationFromViewModel>();
        builder.Services.AddSingleton<NotificationFromPage>();

        builder.Services.AddSingleton<IFirebaseAnalyticsService, FirebaseAnalyticsService>();
        builder.Services.AddSingleton<ISharedService, SharedService>();
        builder.Services.AddSingleton<IFirebaseCrashlyticsService, FirebaseCrashlyticsService>();

        builder.Services.AddTransientPopup<ViewPhotos, ViewPhotosViewModel>();
        builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);

        builder.Services.AddSingleton<SubscriptionPage>();
        builder.Services.AddSingleton<SubscriptionVM>();

#if ANDROID
        builder.Services.AddSingleton<IDateNotificationScheduler, DateNotificationScheduler>();
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        return app;

    }

    private static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android.OnCreate((activity, bundle) => {
                Firebase.FirebaseApp.InitializeApp(activity);
            }));
#endif
        });

        return builder;
    }
}
