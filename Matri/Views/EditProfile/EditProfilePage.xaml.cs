using Matri.ViewModel;

namespace Matri.Views;

public partial class EditProfilePage : ContentPage
{
	public EditProfilePage(EditProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}