using Matri.ViewModel;

namespace Matri.Views;

public partial class NotificationFromPage : ContentPage
{
	public NotificationFromPage(NotificationFromViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}