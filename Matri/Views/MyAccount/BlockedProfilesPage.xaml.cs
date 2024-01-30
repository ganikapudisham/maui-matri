using Matri.ViewModel;

namespace Matri.Views.MyAccount;

public partial class BlockedProfilesPage : ContentPage
{
	public BlockedProfilesPage(BlockedProfilesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}