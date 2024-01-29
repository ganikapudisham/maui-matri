using Matri.ViewModel;
using Syncfusion.Maui.Core.Carousel;

namespace Matri.Views;

public partial class EditAcademicsPage : ContentPage
{
    public EditAcademicsPage(EditAcademicsViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}