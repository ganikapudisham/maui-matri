﻿using Matri.Business;
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
            Routing.RegisterRoute("notificationfrom", typeof(NotificationFromPage));
            Routing.RegisterRoute(nameof(SubscriptionPage), typeof(SubscriptionPage));
        }

        [RelayCommand]
        public async Task ShareToApps()
        {
            var token = await SecureStorage.GetAsync("Token");
            var appDetails = await _serviceManager.GetAppDetails(token);

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
            await _serviceManager.LogoutAsync(token);
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [RelayCommand]
        public async Task DeleteAccount()
        {
            var confirmed = await Shell.Current.CurrentPage.DisplayAlert("Confirm", "Are you sure you want to delete your Account", "Yes", "No");

            if (confirmed)
            {
                var sessionToken = await SecureStorage.GetAsync("Token");
                await _serviceManager.DeleteProfile(sessionToken);
                await Shell.Current.GoToAsync("///LoginPage");
            }
        }
    }
}
