using Matri.ViewModel;
using Syncfusion.Maui.Core.Carousel;

namespace Matri.Views;

public partial class EditReligionPage : ContentPage
{
	public EditReligionPage(EditReligionViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}