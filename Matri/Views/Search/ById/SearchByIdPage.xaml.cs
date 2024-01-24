using Matri.ViewModel;

namespace Matri.Views;

public partial class SearchByIdPage : ContentPage
{
	public SearchByIdPage(SearchByIdViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}