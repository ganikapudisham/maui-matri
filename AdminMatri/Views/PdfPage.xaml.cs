namespace AdminMatri.Views;
using AdminMatri.ViewModel;

public partial class PdfPage : ContentPage
{
	public PdfPage(PdfViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}