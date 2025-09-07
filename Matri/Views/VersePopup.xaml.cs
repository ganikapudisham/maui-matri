using CommunityToolkit.Maui.Views;

namespace Matri.Views;

public partial class VersePopup : Popup
{
    public VersePopup(string title, string message)
    {
        InitializeComponent();
        TitleLabel.Text = title;
        MessageLabel.Text = message;
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        //Close();
    }
}