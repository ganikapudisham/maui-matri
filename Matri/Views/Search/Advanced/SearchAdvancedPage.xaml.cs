using Matri.ViewModel;

namespace Matri.Views;

public partial class SearchAdvancedPage : ContentPage
{
	public SearchAdvancedPage(SearchAdvancedViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}