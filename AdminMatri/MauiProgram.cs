using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using AdminMatri.ViewModel;
using AdminMatri.Views;
using Matri.Business;
using Matri.Business.Impl;
using Matri.Data;
using Matri.Data.Impl;
using Matri.Services;
using Matri.Abstract;
using Syncfusion.Maui.Core.Hosting;
using AdminMatri.Popups;

namespace AdminMatri;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                fonts.AddFont("fa-v4compatibility.ttf", "fontAwsCompatibility");

            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddSingleton<OCRViewModel>();
        builder.Services.AddSingleton<OCRPage>();

        builder.Services.AddSingleton<LeadsViewModel>();
        builder.Services.AddSingleton<LeadsPage>();

        builder.Services.AddSingleton<PdfViewModel>();
        builder.Services.AddSingleton<PdfPage>();

        builder.Services.AddSingleton<IServiceManager, ServiceManager>();
        builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();

        builder.Services.AddSingleton<IFirebaseAnalyticsService, FirebaseAnalyticsService>();
        builder.Services.AddSingleton<ISharedService, SharedService>();
        builder.Services.AddSingleton<IFirebaseCrashlyticsService, FirebaseCrashlyticsService>();

        builder.Services.AddTransientPopup<CallComment, CallCommentViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
