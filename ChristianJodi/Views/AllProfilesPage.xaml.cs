using ChristianJodi.ViewModel;

namespace ChristianJodi.Views;

public partial class AllProfilesPage : ContentPage
{
	public AllProfilesPage(AllProfilesViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
   
}