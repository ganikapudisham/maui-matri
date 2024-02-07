using Matri.Business;
using Matri.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Views.MyAccount;
using PlatformIntegrationDemo.Helpers;

namespace Matri.ViewModel
{
    public partial class AppShellViewModel : ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        //public ICommand ShareCommand { get; }

        public AppShellViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Routing.RegisterRoute("profiledetails", typeof(ProfileDetailsPage));
            Routing.RegisterRoute("searchbyid", typeof(SearchByIdPage));
            Routing.RegisterRoute("searchadvanced", typeof(SearchAdvancedPage));
            Routing.RegisterRoute("searchresults", typeof(SearchResultsPage));
            Routing.RegisterRoute("editprofile", typeof(EditProfilePage));
            Routing.RegisterRoute("changepassword", typeof(ChangePasswordPage));
            Routing.RegisterRoute("blockedprofiles", typeof(BlockedProfilesPage));
            Routing.RegisterRoute("likedprofiles", typeof(LikedProfilesPage));
            Routing.RegisterRoute("newprofiles", typeof(NewProfilesPage));
            Routing.RegisterRoute("visitors", typeof(VisitorsPage));
            Routing.RegisterRoute("requestsreceived", typeof(RequestsReceivedPage));
            Routing.RegisterRoute("requestssent", typeof(RequestsSentPage));

            Routing.RegisterRoute("settings", typeof(SettingsPage));
            Routing.RegisterRoute("aboutus", typeof(AboutUsPage));
            Routing.RegisterRoute("contactus", typeof(ContactUsPage));

            //ShareCommand = new Command<Microsoft.Maui.Controls.View>(OnRequest);
        }

        public async void OnRequest(Microsoft.Maui.Controls.View element)
        {
            try
            {
                var token = await SecureStorage.GetAsync("Token");
                var appDetails = await _serviceManager.GetAppDetails(new Guid(token));
                await Share.RequestAsync(new ShareTextRequest
                {
                    Subject = "ChristianJodi",
                    Text = "Click To Download ChristianJodi App",
                    Uri = appDetails.GooglePlayStoreLink,
                    Title = "Link To Download ChristianJodi App",
                    PresentationSourceBounds = element.GetAbsoluteBounds()
                });
            }
            catch(Exception ex)
            {

            }
        }

        [RelayCommand]

        public async Task ShareToApps()
        {
            var token = await SecureStorage.GetAsync("Token");
            var appDetails = await _serviceManager.GetAppDetails(new Guid(token));

            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "ChristianJodi",
                Text = "Click To Download ChristianJodi App",
                Uri = $"http://play.google.com/store/apps/details?id={AppInfo.PackageName}",
                Title = "Link To Download ChristianJodi App",
            });
        }

        [RelayCommand]
        public async Task LogOut()
        {
            var token = await SecureStorage.GetAsync("Token");
            FlyoutIsPresented = false;
            await _serviceManager.LogoutAsync(new Guid(token));
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
