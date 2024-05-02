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

        [ObservableProperty]
        public bool isBusy = false;

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

            var miniDetails = await _serviceManager.GetShortDetailsOfLoggedInUser(new Guid(sessionToken));

            var guidIdentifier = await _serviceManager.ConvertNumberToGuid(new Guid(sessionToken), miniDetails.WebsiteIdentifier);

            var profileDetailsInput = new ProfileDetailsInput();
            profileDetailsInput.TargetProfileId = guidIdentifier;
            profileDetailsInput.LoggedInId = new Guid(sessionToken);

            var profileDetailsParams = new Dictionary<string, object> { { "ProfileDetailsInput", profileDetailsInput } };

            await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ViewRequestsSent()
        {
            IsBusy = true;
            var requestsSentInputParams = new Dictionary<string, object> { { "requestsSent", "" } };
            await Shell.Current.GoToAsync("requestssent", requestsSentInputParams);
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewRequestsReceived()
        {
            IsBusy = true;
            var requestsReceivedInputParams = new Dictionary<string, object> { { "requestsReceived", "" } };
            await Shell.Current.GoToAsync("requestsreceived", requestsReceivedInputParams);
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewLikedProfiles()
        {
            IsBusy = true;
            var likedInputParams = new Dictionary<string, object> { { "liked", "" } };
            await Shell.Current.GoToAsync("likedprofiles", likedInputParams);
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewBlockedProfiles()
        {
            IsBusy = true;
            var blockedInputParams = new Dictionary<string, object> { { "blocked", "" } };
            await Shell.Current.GoToAsync("blockedprofiles", blockedInputParams);
            IsBusy = false;
        }


        [RelayCommand]
        public async Task ViewVisitors()
        {
            IsBusy = true;
            var visitorsInputParams = new Dictionary<string, object> { { "visitors", "" } };
            await Shell.Current.GoToAsync("visitors", visitorsInputParams);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ViewNewProfiles()
        {
            IsBusy = true;
            var newProfilesInputParams = new Dictionary<string, object> { { "newProfiles", "" } };
            await Shell.Current.GoToAsync("newprofiles", newProfilesInputParams);
            IsBusy = false;
        }
    }
}
