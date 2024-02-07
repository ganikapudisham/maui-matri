using CommunityToolkit.Mvvm.ComponentModel;
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

        [RelayCommand]
        public async Task EditProfile()
        {
            await Shell.Current.GoToAsync("editprofile");
        }

        [RelayCommand]
        public async Task ChangePwd()
        {
            await Shell.Current.GoToAsync("changepassword");
        }

        [RelayCommand]
        public async Task SeeMyProfile()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");

            var miniDetails = await _serviceManager.GetShortDetailsOfLoggedInUser(new Guid(sessionToken));

            var guidIdentifier = await _serviceManager.ConvertNumberToGuid(new Guid(sessionToken), miniDetails.WebsiteIdentifier);

            var profileDetailsInput = new ProfileDetailsInput();
            profileDetailsInput.TargetProfileId = guidIdentifier;
            profileDetailsInput.LoggedInId = new Guid(sessionToken);

            var profileDetailsParams = new Dictionary<string, object> { { "ProfileDetailsInput", profileDetailsInput } };

            await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
        }

        [RelayCommand]
        public async Task ViewRequestsSent()
        {
            await Shell.Current.GoToAsync("requestssent");
        }


        [RelayCommand]
        public async Task ViewRequestsReceived()
        {
            await Shell.Current.GoToAsync("requestsreceived");
        }


        [RelayCommand]
        public async Task ViewLikedProfiles()
        {
            await Shell.Current.GoToAsync("likedprofiles");
        }


        [RelayCommand]
        public async Task ViewBlockedProfiles()
        {
            await Shell.Current.GoToAsync("blockedprofiles");
        }


        [RelayCommand]
        public async Task ViewVisitors()
        {
            await Shell.Current.GoToAsync("visitors");
        }

        [RelayCommand]
        public async Task ViewNewProfiles()
        {
            await Shell.Current.GoToAsync("newprofiles");
        }
    }
}
