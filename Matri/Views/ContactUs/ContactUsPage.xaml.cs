using Matri.ViewModel;

namespace Matri.Views;

public partial class ContactUsPage : ContentPage
{
	public ContactUsPage(ContactUsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}