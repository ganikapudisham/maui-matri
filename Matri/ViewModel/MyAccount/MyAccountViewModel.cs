﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.FontsAwesome;
using Matri.Model;

namespace Matri.ViewModel
{
    public partial class MyAccountViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        public MyAccountViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [ObservableProperty]
        public string likedProfiles = FontAwesomeIcons.ThumbsUp;

        [ObservableProperty]
        public string blockedProfiles = FontAwesomeIcons.ThumbsDown;

        [ObservableProperty]
        public string received = FontAwesomeIcons.Reply;

        [ObservableProperty]
        public string sent = FontAwesomeIcons.FaShare;

        [ObservableProperty]
        public string visitors = FontAwesomeIcons.FaUsers;

        [ObservableProperty]
        public string remember = FontAwesomeIcons.Memory;

        [ObservableProperty]
        public string changePassword = FontAwesomeIcons.Key;

        [ObservableProperty]
        public string viewMyProfile = FontAwesomeIcons.Eye;

        [ObservableProperty]
        public string editMyProfile = FontAwesomeIcons.Edit;

        [ObservableProperty]
        public string next = FontAwesomeIcons.AngleRight;

        [ObservableProperty]
        public bool isBusy = false;

        [ObservableProperty]
        public string deleteAccountIcon = FontAwesomeIcons2.Trash;

        [RelayCommand]
        public async Task EditProfile()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("editprofile");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ChangePwd()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("changepassword");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task SeeMyProfile()
        {
            IsBusy = true;
            var sessionToken = await SecureStorage.GetAsync("Token");

            var miniDetails = await _serviceManager.GetShortDetailsOfLoggedInUser(sessionToken);

            var guidIdentifier = await _serviceManager.ConvertNumberToGuid(sessionToken, miniDetails.WebsiteIdentifier);

            var profileDetailsInput = new ProfileDetailsInput();
            profileDetailsInput.TargetProfileId = guidIdentifier;
            profileDetailsInput.LoggedInId = sessionToken;

            var profileDetailsParams = new Dictionary<string, object> { { "ProfileDetailsInput", profileDetailsInput } };

            await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ViewRequestsSent()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("requestssent");
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewRequestsReceived()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("requestsreceived");
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewLikedProfiles()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("likedprofiles");
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewBlockedProfiles()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("blockedprofiles");
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewVisitors()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("visitors");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ViewNewProfiles()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("newprofiles");
            IsBusy = false;
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
