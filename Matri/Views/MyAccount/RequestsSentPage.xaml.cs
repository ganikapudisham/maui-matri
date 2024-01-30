using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class RequestsSentPage : ContentPage
{
	public RequestsSentPage(RequestsSentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}