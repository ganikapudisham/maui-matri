using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using Matri.FontsAwesome;
using CommunityToolkit.Mvvm.Input;

namespace Matri.ViewModel
{
    public partial class SearchViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public SearchViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [ObservableProperty]
        public string searchIcon=FontAwesomeIcons.FaSearch;
        [ObservableProperty]
        public string searchPlusIcon=FontAwesomeIcons.SearchPlus;

        [RelayCommand]
        public async Task SearchById()
        {
            await Shell.Current.GoToAsync("searchbyid");
        }

        [RelayCommand]
        public async Task SearchAdvanced()
        {
            await Shell.Current.GoToAsync("searchadvanced");
        }

    }
}

