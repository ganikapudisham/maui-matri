using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.CustomExceptions;
using Matri.Business;
using Matri.Abstract;
using Matri.Model;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Matri.Models;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Helper;
using FirebaseAdmin.Messaging;
using Matri.FontsAwesome;
using Plugin.Maui.ScreenSecurity;
using Plugin.LocalNotification;
using System;

namespace Matri.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;
        IFirebaseAnalyticsService _firebaseAnalyticsService;
        IFirebaseCrashlyticsService _firebaseCrashlyticsService;
        private string _deviceToken;
        public LoginViewModel(IServiceManager serviceManager, ISharedService sharedService)
        {
            _serviceManager = serviceManager;
            _sharedService = sharedService;
            _firebaseAnalyticsService = ServiceHelper.GetService<IFirebaseAnalyticsService>();
            _firebaseCrashlyticsService = ServiceHelper.GetService<IFirebaseCrashlyticsService>();

            WeakReferenceMessenger.Default.Register<PushNotificationReceived>(this, (r, m) =>
            {
                string msg = m.Value;
                NavigateToPage();
            });

            ReadFireBaseAdminSdk();

            Task.Run(async () => { await ShowNewVersionAvailableMessage(); });

            ScreenSecurity.Default.ActivateScreenSecurityProtection();
        }
#if DEBUG
        [ObservableProperty]
        public string eMobile = "lucky";

        [ObservableProperty]
        public string ePassword = "lucky";
#else
        [ObservableProperty]
        public string eMobile;

        [ObservableProperty]
        public string ePassword;
#endif
        [ObservableProperty]
        public bool isBusy;

        [ObservableProperty]
        public bool isHiddenPassword = true;

        [ObservableProperty]
        public string passwordVisibilityIcon = FontAwesomeIcons.EyeSlash;

        [ObservableProperty]
        public bool newVersionPromptVisibility = false;

        [ObservableProperty]
        public string newVersionAvailableMessage;

        [ObservableProperty]
        public string currentAppVersion;

        [ObservableProperty]
        public string bgColor = "White";

        [ObservableProperty]
        public string customerCareNumber = "";

        [RelayCommand]
        public async Task Login()
        {
            _firebaseAnalyticsService?.Log("visited");

            if (string.IsNullOrWhiteSpace(EMobile))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide mobile number", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EPassword))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide password", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var session = await _serviceManager.LoginAsync(EMobile, EPassword);

                await SecureStorage.SetAsync("Token", session.SessionToken.ToString());

                //Save app Base wether If Hindu Users

                //var appDetails = await _serviceManager.GetAppDetails(session.SessionToken);

                var sessionToken = await SecureStorage.GetAsync("Token");
                var user = await _serviceManager.GetUserData(sessionToken);
                var masterData = await _serviceManager.GetMasterData(sessionToken);

                _sharedService.Add<Profile>("LoggedInUser", user);
                _sharedService.Add<MDD>("MasterData", masterData);
                _sharedService.AddBool("ShowHinduFields", false);
                _sharedService.AddBool("SubScriptionType", session.SubscriptionActive);
                IsBusy = false;

                var fcmToken = new FCMToken();
                fcmToken.Token = "";

                if (Preferences.ContainsKey("DeviceToken"))
                {
                    _deviceToken = Preferences.Get("DeviceToken", "");
                    fcmToken.Token = _deviceToken;
                }

                var deviceTokenSaved = await _serviceManager.CreateUpdateDeviceToken(sessionToken, fcmToken);

                var birthdayNotification = new NotificationRequest
                {
                    NotificationId = 1,
                    Title = "Testing",
                    Subtitle = "Hello",
                    Description = "Its me",
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5),
                        NotifyRepeatInterval = TimeSpan.FromMinutes(1),
                        RepeatType = NotificationRepeat.TimeInterval
                    }//,
                    //Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                    //{

                    //},
                    //iOS = new Plugin.LocalNotification.iOSOption.iOSOptions
                    //{

                    //}

                    //NotifyTime = new DateTime(2025, 01, 01),
                    //NotifyRepeatInterval = DateTime.Now.AddYears(1) - DateTime.Now
                };

                //LocalNotificationCenter.Current.Show(birthdayNotification);
                //LocalNotificationCenter.Current.Show(christmasNotification);
                //LocalNotificationCenter.Current.Show(easterNotification);
                //LocalNotificationCenter.Current.Show(newYearNotification);


                await Shell.Current.GoToAsync("//AllProfilesPage");
            }
            catch (MatriInternetException exception)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
                IsBusy = false;
            }
            catch (Exception exception)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task ForgotPassword()
        {
            await Shell.Current.GoToAsync("//ForgotPasswordPage");
        }

        [RelayCommand]
        public async Task Register()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async void ReadFireBaseAdminSdk()
        {
#if ANDROID
            var stream = await FileSystem.OpenAppPackageFileAsync("admin_sdk.json");
            var reader = new StreamReader(stream);

            var jsonContent = reader.ReadToEnd();


            if (FirebaseMessaging.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonContent)
                });
            }
#endif
        }

        private async Task NavigateToPage()
        {
            if (Preferences.ContainsKey("NotificationFrom"))
            {
                string userIdentifier = Preferences.Get("NotificationFrom", "");
                if (!string.IsNullOrEmpty(userIdentifier))
                {
                    var notificationFromParams = new Dictionary<string, object> { { "NotificationReceivedFromInput", userIdentifier } };
                    await Shell.Current.GoToAsync("notificationfrom", notificationFromParams);
                }
                Preferences.Remove("NotificationFrom");
            }
        }

        [RelayCommand]
        public void TogglePassword()
        {
            IsHiddenPassword = false;
            PasswordVisibilityIcon = FontAwesomeIcons.Eye;

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Delay(500).ContinueWith(async (t) =>
            {
                IsHiddenPassword = true;
                PasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;
            }, cancellationToken);
        }

        public async Task ShowNewVersionAvailableMessage()
        {
            IsBusy = true;
            var appDetails = await _serviceManager.GetAppDetails(Guid.Empty.ToString());

            var UserAppVersion = AppInfo.Current.VersionString;
            var localV = AppInfo.Current.Version.ToString();
            var AppVersionLatest = appDetails.LatestVersion;
            var GooglePlayStoreAppId = appDetails.GooglePlayStoreLink;

            CurrentAppVersion = $"Version {AppInfo.Current.VersionString.Trim()}";

            CustomerCareNumber = appDetails.WAAdminNumber;

            if (appDetails.LatestVersion.Trim() != AppInfo.Current.VersionString.Trim())
            {
                NewVersionPromptVisibility = true;
                BgColor = "Brown";
                NewVersionAvailableMessage = $"Click here to install latest version {AppVersionLatest}";
            }
            IsBusy = false;
        }

        [RelayCommand]
        public async Task UpdateApp()
        {
            var launcherOpened = await Launcher.Default.OpenAsync(new Uri($"{Constants.PlayStoreAppUrl}{AppInfo.PackageName}"));

            if (launcherOpened)
            {

            }
        }

        [RelayCommand]
        public void CallCustomer()
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(CustomerCareNumber);
            }
        }

        [RelayCommand]
        public async Task Whatsapp()
        {
            try
            {
                bool supportsUri = await Launcher.Default.CanOpenAsync($"whatsapp://send?phone=+91{CustomerCareNumber}");

                if (supportsUri)
                {
                    var message = "";
                    await Launcher.Default.OpenAsync($"whatsapp://send?phone=+91{CustomerCareNumber}&text={message}");
                }

                else
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Failed to open WhatsApp.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task ShareToApps()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "ChristianJodi",
                Text = "Click To Download ChristianJodi App",
                Uri = $"http://play.google.com/store/apps/details?id={AppInfo.PackageName}",
                Title = "Link To Download ChristianJodi App",
            });
        }
    }
}
