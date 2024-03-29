﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.CustomExceptions;
using Matri.Business;
using Matri.Abstract;
using Matri.Model;

namespace Matri.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;
        public LoginViewModel(IServiceManager serviceManager, ISharedService sharedService)
        {
            _serviceManager = serviceManager;
            _sharedService = sharedService;
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
            //var fireBase = DependencyService.Get<IFirebaseAnalytics>();
            //fireBase?.LogEvent("visited");

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
    }
}
