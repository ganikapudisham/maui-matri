using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Matri.ViewModel
{
    public class SearchViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public SearchViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task SearchByIdCommand()
        {
            await Shell.Current.GoToAsync("searchbyid");
        }

        public async Task SearchAdvancedCommand()
        {
            await Shell.Current.GoToAsync("searchadvanced");
        }

    }
}

