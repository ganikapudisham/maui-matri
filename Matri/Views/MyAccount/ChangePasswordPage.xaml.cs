using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage(ChangePasswordViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}