using AdminMatri.ViewModel;
using CommunityToolkit.Maui.Views;

namespace AdminMatri.Popups;

public partial class CallComment : Popup
{
	public CallComment(CallCommentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}