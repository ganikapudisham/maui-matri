using Matri.ViewModel;

namespace Matri
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxcc3VURmBdVEd2XUc=");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
