using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdminMatri.ViewModel
{
    public partial class LeadsViewModel : ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        [ObservableProperty]
        public bool isBusy = true;

        public LeadsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Task.Run(() => { this.GetLeads(); });
        }

        public async Task<bool> GetLeads()
        {
            IsBusy = true;
            var token = await SecureStorage.GetAsync("Token");
            try
            {
                var dbProfilesWithPaging = await _serviceManager.GetLeads(token);
                IsBusy = false;
                return true;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                return false;
            }
        }
    }
}
