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

            //int number1 = 3000;
            //int number2 = 0;
            //try
            //{
            //    Console.WriteLine(number1 / number2);
            //}
            //catch (DivideByZeroException dbze)
            //{
            //    _firebaseCrashlyticsService.Log(dbze);
            //}

            WeakReferenceMessenger.Default.Register<PushNotificationReceived>(this, (r, m) =>
            {
                string msg = m.Value;
            });

            if (Preferences.ContainsKey("DeviceToken"))
            {
                _deviceToken = Preferences.Get("DeviceToken", "");
            }
            ReadFireBaseAdminSdk();
            //sendNotification();
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

                var appDetails = await _serviceManager.GetAppDetails(session.SessionToken);

                var sessionToken = await SecureStorage.GetAsync("Token");
                var user = await _serviceManager.GetUserData(new Guid(sessionToken));
                var masterData = await _serviceManager.GetMasterData(new Guid(sessionToken));

                _sharedService.Add<Profile>("LoggedInUser", user);
                _sharedService.Add<MDD>("MasterData", masterData);
                _sharedService.AddBool("ShowHinduFields", appDetails.ShowHinduFields);
                _sharedService.AddBool("SubScriptionType", session.SubscriptionActive);
                IsBusy = false;
                var fcmToken = new FCMToken();
                fcmToken.Token = _deviceToken;
                var deviceTokenSaved = await _serviceManager.CreateUpdateDeviceToken(new Guid(sessionToken),fcmToken);
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

        private async Task sendNotification()
        {
            var androidNotificationObject = new Dictionary<string, string>();
            androidNotificationObject.Add("NavigationID", "2");

            var iosNotificationObject = new Dictionary<string, object>();
            iosNotificationObject.Add("NavigationID", "2");

            var pushNotificationRequest = new PushNotificationRequest
            {
                notification = new NotificationMessageBody
                {
                    title = "Notification Title",
                    body = "Notification body"
                },
                data = androidNotificationObject,
                registration_ids = new List<string> { _deviceToken }
            };

            var obj = new Message
            {
                Token = _deviceToken,
                Notification = new Notification
                {
                    Title = "Tilte",
                    Body = "message body"
                },
                Data = androidNotificationObject,
                Apns = new ApnsConfig()
                {
                    Aps = new Aps
                    {
                        Badge = 15,
                        CustomData = iosNotificationObject,
                    }
                }
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(obj);
        }
    }
}
