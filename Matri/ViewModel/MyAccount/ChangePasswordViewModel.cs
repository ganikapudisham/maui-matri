﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public partial class ChangePasswordViewModel : ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public string currentPassword;

        [ObservableProperty]
        public string newPassword ;

        [ObservableProperty]
        public string confirmPassword;

        [ObservableProperty]
        public bool isBusy;
        public ChangePasswordViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [RelayCommand]
        public async Task ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Enter Current Password", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Enter New Password", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Confirm New Password", "OK");
                return;
            }

            var token = await SecureStorage.GetAsync("Token");
            var password = new Password();
            password.New = NewPassword;
            password.Current = CurrentPassword;
            password.Confirm = ConfirmPassword;

            try
            {
                IsBusy = true;
                var result = await _serviceManager.ChangePasswordAsync(new Guid(token), password);
                IsBusy = false;
                if (result)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Password Changed", "OK");
                }
                else
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please try again", "OK");
                }
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
        }
    }
}

