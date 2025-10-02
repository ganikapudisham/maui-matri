using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.CustomExceptions;
using Matri.Business;
using Matri.Abstract;
using Matri.Model;
using Matri.Models;
using CommunityToolkit.Mvvm.Messaging;
using Matri.FontsAwesome;
using Plugin.Maui.ScreenSecurity;
using Matri.Views;

namespace Matri.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    IServiceManager _serviceManager;
    ISharedService _sharedService;
    //IFirebaseAnalyticsService _firebaseAnalyticsService;
    //IFirebaseCrashlyticsService _firebaseCrashlyticsService;
    private string _deviceId;
    private string _fcmToken;
    private const int NotificationIdBirthday = 307;
    //private readonly Abstract.IDateNotificationScheduler _birthdayService;

    public LoginViewModel(IServiceManager serviceManager, ISharedService sharedService)
    {
        _serviceManager = serviceManager;
        _sharedService = sharedService;
        //_firebaseAnalyticsService = ServiceHelper.GetService<IFirebaseAnalyticsService>();
        //_firebaseCrashlyticsService = ServiceHelper.GetService<IFirebaseCrashlyticsService>();
        //_birthdayService = ServiceHelper.GetService<Abstract.IDateNotificationScheduler>();

        WeakReferenceMessenger.Default.Register<PushNotificationReceived>(this, (r, m) =>
        {
            string msg = m.Value;
            NavigateToPage();
        });

        //ReadFireBaseAdminSdk();

        //Task.Run(async () => { await ShowNewVersionAvailableMessage(); });

        ScreenSecurity.Default.ActivateScreenSecurityProtection();
    }
#if DEBUG
    [ObservableProperty]
    public string eMobile = "lucky";//alex

    [ObservableProperty]
    public string ePassword = "123lucky";//alex123
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

    [ObservableProperty]
    public bool showSubscriptionBanner;

    [ObservableProperty]
    public string subscriptionMessage;

    [RelayCommand]
    public async Task Login()
    {
        //_firebaseAnalyticsService?.Log("visited");

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
            fcmToken.DeviceId = "";
            fcmToken.FcmToken = "";

            if (Preferences.ContainsKey("DeviceId"))
            {
                fcmToken.DeviceId = Preferences.Get("DeviceId", ""); ;
            }

            if (Preferences.ContainsKey("FcmToken"))
            {
                fcmToken.FcmToken = Preferences.Get("FcmToken", ""); ;
            }

            var deviceTokenSaved = await _serviceManager.CreateUpdateDeviceToken(sessionToken, fcmToken);

            //_birthdayService.CancelNotification("tag");
            var birthDate = DateTime.Parse(session.BirthDate);
            //_birthdayService.ScheduleNotification(birthDate, "Happy Birthday", "Happy Birthday", "tagBirthday");

            if (!session.SubscriptionActive)
            {
                ShowRenewSubscriptionMessage();
            }

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

    private void ShowRenewSubscriptionMessage()
    {
        ShowSubscriptionBanner = true;
        BgColor = "Brown";
        SubscriptionMessage = $"Your Subscription has expired. Click here to renew.";
    }

    [RelayCommand]
    public async Task ShowSubscriptionPlans()
    {
        //await Shell.Current.GoToAsync("//ForgotPasswordPage");
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

//    private async void ReadFireBaseAdminSdk()
//    {
//#if ANDROID
//        var stream = await FileSystem.OpenAppPackageFileAsync("admin_sdk.json");
//        var reader = new StreamReader(stream);

//        var jsonContent = reader.ReadToEnd();


//        if (FirebaseMessaging.DefaultInstance == null)
//        {
//            FirebaseApp.Create(new AppOptions()
//            {
//                Credential = GoogleCredential.FromJson(jsonContent)
//            });
//        }
//#endif
//    }

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

        var newVersionIsAvailable = VersionHelper.IsNewVersionAvailable(UserAppVersion, AppVersionLatest);

        if (newVersionIsAvailable)
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

    internal void OnPageAppearing()
    {
        Task.Run(async () => { await ShowNewVersionAvailableMessage(); });

        var subscriptionActive = _sharedService.GetBool("SubScriptionType");

        if (subscriptionActive != true)
        {
            ShowRenewSubscriptionMessage();
        }
    }

    [RelayCommand]
    public async Task Subscribe()
    {
        await Shell.Current.GoToAsync(nameof(SubscriptionPage));
    }
}
