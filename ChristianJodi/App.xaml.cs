using ChristianJodi.ViewModel;

namespace ChristianJodi
{
    public partial class App : Application
    {
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWX5fdXRcQ2hYVUBwWEA=");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
