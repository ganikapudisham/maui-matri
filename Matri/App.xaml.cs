using Matri.ViewModel;

namespace Matri
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU2OTQ4NEAzMjM3MmUzMDJlMzBoOFljZjAveTgrVDBkWTRpOW1GZFZSYzNXbUw1THE3SUJsSEpraTNxdnBvPQ==");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
