using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
using Newtonsoft.Json;
using Matri.FontsAwesome;

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

        [ObservableProperty]
        public string currentPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;

        [ObservableProperty]
        public string newPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;

        [ObservableProperty]
        public string confirmPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;

        [ObservableProperty]
        public bool currentPasswordHidden = true;

        [ObservableProperty]
        public bool newPasswordHidden = true;

        [ObservableProperty]
        public bool confirmPasswordHidden = true;

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
                var result = await _serviceManager.ChangePasswordAsync(token, password);
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

        [RelayCommand]
        public void ToggleCurrentPassword()
        {
            CurrentPasswordHidden = false;
            CurrentPasswordVisibilityIcon = FontAwesomeIcons.Eye;

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Delay(500).ContinueWith(async (t) =>
            {
                CurrentPasswordHidden = true;
                CurrentPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;
            }, cancellationToken);
        }

        [RelayCommand]
        public void ToggleNewPassword()
        {
            NewPasswordHidden = false;
            NewPasswordVisibilityIcon = FontAwesomeIcons.Eye;

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Delay(500).ContinueWith(async (t) =>
            {
                NewPasswordHidden = true;
                NewPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;
            }, cancellationToken);
        }

        [RelayCommand]
        public void ToggleConfirmPassword()
        {
            ConfirmPasswordHidden = false;
            ConfirmPasswordVisibilityIcon = FontAwesomeIcons.Eye;

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Delay(500).ContinueWith(async (t) =>
            {
                ConfirmPasswordHidden = true;
                ConfirmPasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;
            }, cancellationToken);
        }
    }
}

