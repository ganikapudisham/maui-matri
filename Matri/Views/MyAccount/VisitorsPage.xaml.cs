using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class VisitorsPage : ContentPage
{
	public VisitorsPage(VisitorsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}