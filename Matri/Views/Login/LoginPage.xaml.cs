using Matri.ViewModel;

namespace Matri.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LoginViewModel vm)
        {
            vm.OnPageAppearing(); // Call your ViewModel method
        }
    }
}