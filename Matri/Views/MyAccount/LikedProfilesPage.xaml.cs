using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class LikedProfilesPage : ContentPage
{
	public LikedProfilesPage(LikedProfilesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}