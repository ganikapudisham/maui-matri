using Matri.ViewModel;

namespace Matri.Views;

public partial class MyAccountPage : ContentPage
{
	public MyAccountPage(MyAccountViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}