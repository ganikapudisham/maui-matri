using ChristianJodi.Business;
using ChristianJodi.Business.Impl;
using ChristianJodi.Data;
using ChristianJodi.Data.Impl;
using ChristianJodi.ViewModel;
using ChristianJodi.Views;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;
using ChristianJodi.Services;
using Microsoft.Extensions.Logging;

namespace ChristianJodi
{
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
                    fonts.AddFont("fa-brands-400.ttf", "FontBrands");
                    fonts.AddFont("FontAwesome5Solid.otf", "FontAwesomeSolid");
                    //fonts.AddFont("fa-solid-900.ttf", "FontSolid");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
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

            //builder.Services.AddSingleton<ICloseApplication, CloseApplication>();
            //builder.Services.AddSingleton<IFirebaseAnalytics, FirebaseAnalytics>();
            //builder.Services.AddSingleton<IAppVersionProvider, AppVersionProvider>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
