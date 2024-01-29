using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.FontsAwesome;

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
    }
}
