using Matri.ViewModel;

namespace Matri.Views;

public partial class AllProfilesPage : ContentPage
{
    public AllProfilesPage(AllProfilesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}