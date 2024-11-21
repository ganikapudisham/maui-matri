using AdminMatri.ViewModel;

namespace AdminMatri
{
    public partial class App : Application
    {
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU2OTQ4NEAzMjM3MmUzMDJlMzBoOFljZjAveTgrVDBkWTRpOW1GZFZSYzNXbUw1THE3SUJsSEpraTNxdnBvPQ==");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
