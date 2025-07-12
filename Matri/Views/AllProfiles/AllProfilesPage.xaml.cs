using Matri.ViewModel;

namespace Matri.Views;

public partial class AllProfilesPage : ContentPage
{
    public AllProfilesPage(AllProfilesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AllProfilesViewModel vm)
        {
            vm.OnPageAppearing(); // Call your ViewModel method
        }
    }
}