using ChristianJodi.ViewModel;
using CommunityToolkit.Mvvm.Input;

namespace ChristianJodi
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        [RelayCommand]
        public async Task LogOut()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}
