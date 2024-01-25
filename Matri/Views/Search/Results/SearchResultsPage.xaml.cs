using Matri.ViewModel;

namespace Matri.Views;

public partial class SearchResultsPage : ContentPage
{
	public SearchResultsPage(SearchResultsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}