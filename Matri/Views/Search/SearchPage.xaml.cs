using Matri.ViewModel;

namespace Matri.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}