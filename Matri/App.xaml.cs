using Matri.ViewModel;
using Plugin.Maui.ScreenSecurity;

namespace Matri;

public partial class App : Application
{
    public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
    public App(AppShellViewModel viewModel)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWH9ecnRQQ2NYVkJ+WkpWYEg=");
        InitializeComponent();
        MainPage = new AppShell(viewModel);
    }

    protected override void OnAppLinkRequestReceived(Uri uri)
    {
        base.OnAppLinkRequestReceived(uri);

        if (uri != null)
        {
            // Example: christianjodi://payment-success
            if (uri.Host == "payment-success")
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync("///LoginPage");
                });
            }
            else if (uri.Host == "payment-failed")
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync("///LoginPage");
                });
            }
        }
    }
}
