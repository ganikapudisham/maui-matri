using ChristianJodi.Business;
using ChristianJodi.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChristianJodi.ViewModel
{
    public partial class AppShellViewModel :ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool isFlyoutPresented;
        public AppShellViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Routing.RegisterRoute("profiledetails", typeof(ProfileDetailsPage));
        }

        [RelayCommand]
        public async Task LogOut()
        {
            var token = await SecureStorage.GetAsync("Token");
            IsFlyoutPresented = false;
            await _serviceManager.LogoutAsync(new Guid(token));
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
