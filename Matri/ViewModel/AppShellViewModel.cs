using Matri.Business;
using Matri.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Layouts;
using Matri.Views.MyAccount;

namespace Matri.ViewModel
{
    public partial class AppShellViewModel :ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        public AppShellViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Routing.RegisterRoute("profiledetails", typeof(ProfileDetailsPage));
            //Routing.RegisterRoute("settings", typeof(SettingsPage));
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
