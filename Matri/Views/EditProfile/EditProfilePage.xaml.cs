using Matri.ViewModels;

namespace Matri.Views;

public partial class EditProfilePage : TabbedPage
{
	public EditProfilePage(EditProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}