using ChristianJodi.ViewModel;

namespace ChristianJodi.Views;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(ForgotPasswordViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}