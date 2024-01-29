using Matri.ViewModel;

namespace Matri.Views;

public partial class EditLifeStylePage : ContentPage
{
	public EditLifeStylePage(EditLifeStyleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}