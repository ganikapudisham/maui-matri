using Matri.ViewModel;

namespace Matri.Views;

public partial class EditHoroscopePage : ContentPage
{
	public EditHoroscopePage(EditHoroscopeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}