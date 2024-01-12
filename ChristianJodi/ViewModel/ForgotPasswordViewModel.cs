using ChristianJodi.Business;
using ChristianJodi.CustomExceptions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace ChristianJodi.ViewModel
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
            if (string.IsNullOrWhiteSpace(emailId))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please specify emailId", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var result = await _serviceManager.ForgetPasswordAsync(emailId);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Password sent to your EmailId, please check email", "OK");
                await Shell.Current.GoToAsync("//loginPage");
            }
            catch (ChristianJodiInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
            }
            catch (System.Exception exception)
            {
                IsBusy = false;
                //var jsonResponse = exception.Message;
                //var errorMessage = JsonConvert.DeserializeObject<ChristianJodiException>(jsonResponse);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
            }
            IsBusy = false;
        }
    }
}
