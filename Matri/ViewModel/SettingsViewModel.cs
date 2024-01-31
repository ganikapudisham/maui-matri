using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
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
        public  int userAppVersionNumber;

        [RelayCommand]
        public async Task NavigateToPlayStore()
        {
            await Launcher.OpenAsync(new Uri($"market://details?id={GooglePlayStoreAppId}"));
        }

        public async Task LoadTasks()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var appDetails = await _serviceManager.GetAppDetails(new Guid(sessionToken));

            var userApp = DependencyService.Get<IAppVersionProvider>();

            UserAppVersion = userApp.AppVersion;
            AppVersionLatest = appDetails.LatestVersion;
            GooglePlayStoreAppId = userApp.GooglePlayStoreAppId;
            UserAppVersionNumber = userApp.AppVersionNumber;

            if (appDetails.LatestVersionNumber > UserAppVersionNumber)
            {
                UpdateAvailable = true;
            }
        }
    }
}
