using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdminMatri.ViewModel
{
    public partial class AppShellViewModel : ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        public AppShellViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
    }
}
