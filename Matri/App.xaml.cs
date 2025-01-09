using Matri.ViewModel;
using Plugin.Maui.ScreenSecurity;

namespace Matri
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5eeXVWRmZdVkdyWUM=");
            InitializeComponent();
            MainPage = new AppShell(viewModel);
        }
    }
}
