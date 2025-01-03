using Matri.ViewModel;
using CommunityToolkit.Maui.Views;

namespace Matri.Views.Popups;

public partial class ViewPhotos : Popup
{
    private ViewPhotosViewModel _viewModel;
    public ViewPhotos(ViewPhotosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
        viewModel.OnClose += Close;
    }

    public void Dispose()
    {
        _viewModel.OnClose -= Close;
    }

    private async Task Close(ViewPhotos result)
    {
        await CloseAsync(result, CancellationToken.None);
    }
}