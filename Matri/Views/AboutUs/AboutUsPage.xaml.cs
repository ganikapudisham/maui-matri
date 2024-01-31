using Matri.ViewModel;

namespace Matri.Views;

public partial class AboutUsPage : ContentPage
{
	public AboutUsPage(AboutUsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}