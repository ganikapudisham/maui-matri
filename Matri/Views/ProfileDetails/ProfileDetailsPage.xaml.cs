using Matri.ViewModel;

namespace Matri.Views;

public partial class ProfileDetailsPage : ContentPage
{
	public ProfileDetailsPage(ProfileDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}