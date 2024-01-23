using Matri.Business;
using Matri.CustomExceptions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public partial class ForgotPasswordViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public ForgotPasswordViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [ObservableProperty]
        public string emailId;

        [ObservableProperty]
        public bool isBusy = false;

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [RelayCommand]
        public async Task ForgotPassword()
        {
            if (string.IsNullOrWhiteSpace(EmailId))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please specify emailId", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var result = await _serviceManager.ForgetPasswordAsync(EmailId);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Password sent to your EmailId, please check email", "OK");
                await Shell.Current.GoToAsync("//loginPage");
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
            }
            catch (System.Exception exception)
            {
                IsBusy = false;
                //var jsonResponse = exception.Message;
                //var errorMessage = JsonConvert.DeserializeObject<MatriException>(jsonResponse);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
            }
            IsBusy = false;
        }
    }
}
