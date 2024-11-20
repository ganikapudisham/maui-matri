using AdminMatri.ViewModel;

namespace AdminMatri.Views;

public partial class LeadsPage : ContentPage
{
	public LeadsPage(LeadsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}