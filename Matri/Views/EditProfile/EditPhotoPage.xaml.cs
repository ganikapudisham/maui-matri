using Matri.ViewModel;

namespace Matri.Views;

public partial class EditPhotoPage : ContentPage
{
	public EditPhotoPage(EditPhotoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}