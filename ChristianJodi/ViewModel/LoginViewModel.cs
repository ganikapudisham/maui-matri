using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChristianJodi.CustomExceptions;
using ChristianJodi.Business;

namespace ChristianJodi.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public LoginViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
#if DEBUG
        [ObservableProperty]
        public string eMobile = "icm";

        [ObservableProperty]
        public string ePassword = "icm";
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
            var fireBase = DependencyService.Get<IFirebaseAnalytics>();
            fireBase?.LogEvent("visited");

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

            await Authenticate(EMobile, EPassword);

            IsBusy = false;
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

        private async Task Authenticate(string mobile, string passWord)
        {
            try
            {
                var session = await _serviceManager.LoginAsync(mobile, passWord);
                await SecureStorage.SetAsync("Token", session.SessionToken.ToString());

                //Save app Base wether If Hindu Users

                var appDetails = await _serviceManager.GetAppDetails(session.SessionToken);
                await SecureStorage.SetAsync("ShowHinduFields", appDetails.ShowHinduFields.ToString());

                await SecureStorage.SetAsync("SubScriptionType", session.SubscriptionActive.ToString());

                await Shell.Current.GoToAsync("//AllProfilesPage");
            }
            catch (ChristianJodiInternetException exception)
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

    }
}
