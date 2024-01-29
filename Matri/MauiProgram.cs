using CommunityToolkit.Maui;
using Matri.Business;
using Matri.Business.Impl;
using Matri.Data;
using Matri.Data.Impl;
using Matri.ViewModel;
using Matri.ViewModels;
using Matri.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace Matri
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");

                    fonts.AddFont("Montserrat-Bold.ttf", "Montserrat-Bold");
                    fonts.AddFont("Montserrat-Medium.ttf", "Montserrat-Medium");
                    fonts.AddFont("Montserrat-Regular.ttf", "Montserrat-Regular");
                    fonts.AddFont("Montserrat-Semibold.ttf", "Montserrat-Semibold");
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

            //builder.Services.AddSingleton<SettingsViewModel>();
            //builder.Services.AddSingleton<SettingsPage>();

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
