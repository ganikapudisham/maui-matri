using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class NewProfilesPage : ContentPage
{
	public NewProfilesPage(NewProfilesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}