using ChristianJodi.Business;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChristianJodi.ViewModel
{
    public partial class LogoutViewModel 
    {
        IServiceManager _serviceManager;
        public LogoutViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Task.Run(() => this.LogoutAsync());
        }

        public async Task LogoutAsync()
        {
            var token = await SecureStorage.GetAsync("Token");
            await _serviceManager.LogoutAsync(new Guid(token));

            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}
