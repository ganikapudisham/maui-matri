using ChristianJodi.ViewModel;

namespace ChristianJodi.Views;

public partial class ProfileDetailsPage : ContentPage
{
	public ProfileDetailsPage(ProfileDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}