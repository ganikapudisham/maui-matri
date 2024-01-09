using ChristianJodi.ViewModel;

namespace ChristianJodi
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
