using AdminMatri.ViewModel;

namespace AdminMatri
{
    public partial class App : Application
    {
        public App(AppShellViewModel viewModel)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI2MzEzNkAzMjM4MmUzMDJlMzBibmZJcTJiL0cvWTE3cmxhanEwankwOGlnT3lEdFd6ZFgxK1hvVDZoem1BPQ==");
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
