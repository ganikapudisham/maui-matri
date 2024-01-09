using ChristianJodi.ViewModel;

namespace ChristianJodi.Views;

public partial class AuthenticatedPage : ContentPage
{
	public AuthenticatedPage(AuthenticatedViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}