using AdminMatri.ViewModel;

namespace AdminMatri.Views;

public partial class OCRPage : ContentPage
{
	public OCRPage(OCRViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}