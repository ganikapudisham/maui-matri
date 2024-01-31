using Matri.ViewModel;

namespace Matri
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWX5fdXRcQ2hYVUBwWEA=");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
