using Matri.ViewModel;
using CommunityToolkit.Maui.Views;

namespace Matri.Views.Popups;

public partial class ViewPhotos : Popup
{
	public ViewPhotos(ViewPhotosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}