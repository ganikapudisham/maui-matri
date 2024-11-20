using AdminMatri.ViewModel;

namespace AdminMatri
{
    public partial class App : Application
    {
        public App(AppShellViewModel viewModel)
        {
            InitializeComponent();

            MainPage = new AppShell(viewModel);
        }
    }
}
