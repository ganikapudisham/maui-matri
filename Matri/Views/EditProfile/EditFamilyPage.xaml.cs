using Matri.ViewModel;

namespace Matri.Views;

public partial class EditFamilyPage : ContentPage
{
	public EditFamilyPage(EditFamilyViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}