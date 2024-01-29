using Matri.ViewModel;

namespace Matri.Views;

public partial class EditExpectationsPage : ContentPage
{
	public EditExpectationsPage(EditExpectationsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}