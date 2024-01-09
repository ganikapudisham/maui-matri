
using ChristianJodi.ViewModel;
using CommunityToolkit.Mvvm.Input;

namespace ChristianJodi.Views;
public partial class LoginPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext =  viewModel;
    }

	[RelayCommand]
	public async Task Login()
	{
        await Shell.Current.GoToAsync("//authenticatedPage");
    }

    [RelayCommand]
    public async Task ForgotPassword()
    {
        await Shell.Current.GoToAsync("//ForgotPasswordPage");
    }

    [RelayCommand]
    public async Task Register()
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}