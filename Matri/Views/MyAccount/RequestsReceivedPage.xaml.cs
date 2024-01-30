using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class RequestsReceivedPage : ContentPage
{
	public RequestsReceivedPage(RequestsReceivedViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}