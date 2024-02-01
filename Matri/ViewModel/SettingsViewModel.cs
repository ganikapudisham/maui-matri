using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.FontsAwesome;
using Matri.Model;
using System.Collections.ObjectModel;

namespace Matri.ViewModel
{
    public partial class SettingsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        public SettingsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

            Task.Run(async () => { await LoadTasks(); });
        }

        [ObservableProperty]
        public string userAppVersion;
        [ObservableProperty]
        public string appVersionLatest;
        [ObservableProperty]
        public bool updateAvailable;
        [ObservableProperty]
        public string googlePlayStoreAppId;
        [ObservableProperty]
        public int userAppVersionNumber;

        [ObservableProperty]
        public string myAppVersionIcon = String.Format("{0} {1}", FontAwesomeIcons.Person, FontAwesomeIcons.AppStore);

        [ObservableProperty]
        public string latestAppVersionIcon = FontAwesomeIcons.AppStore;

        [RelayCommand]
        public async Task UpdateApp()
        {
            var launcherOpened = await Launcher.Default.OpenAsync(new Uri($"http://play.google.com/store/apps/details?id={AppInfo.PackageName}"));

            if (launcherOpened)
            {

            }
        }

        public async Task LoadTasks()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var appDetails = await _serviceManager.GetAppDetails(new Guid(sessionToken));

            UserAppVersion = AppInfo.Current.VersionString;
            AppVersionLatest = appDetails.LatestVersion;
            GooglePlayStoreAppId = appDetails.GooglePlayStoreLink;

            if (appDetails.LatestVersion.Trim() != AppInfo.Current.VersionString.Trim())
            {
                UpdateAvailable = true;
            }
        }
    }
}
