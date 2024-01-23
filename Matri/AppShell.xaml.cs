using CommunityToolkit.Mvvm.Input;
using Matri.ViewModel;

namespace Matri
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
