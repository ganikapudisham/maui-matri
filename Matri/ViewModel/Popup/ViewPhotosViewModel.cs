using Matri.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;
using System.Windows.Input;
using Matri.Model;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Carousel;

namespace Matri.ViewModel;

public partial class ViewPhotosViewModel : ObservableObject
{
    IServiceManager _serviceManager;

    private ObservableCollection<SfCarouselItem> _profilePhotos = new ObservableCollection<SfCarouselItem>();
    public ViewPhotosViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task LoadPhotos(IDictionary<string, object> query)
    {
        ProfilePhotos.Clear();

        var queryParam = query[nameof(ProfileDetailsInput)] as ProfileDetailsInput;
        var targetId = queryParam.TargetProfileId;
        var sourceId = queryParam.LoggedInId;

        var profileDetails = await _serviceManager.GetProfileById(sourceId, targetId);

        ProfilePhotos = new ObservableCollection<SfCarouselItem>();

        foreach (var pt in profileDetails.Photos)
        {
            ProfilePhotos.Add(new SfCarouselItem() { ItemContent = new Image() { Source = pt.Name } });
        }
    }

    public ObservableCollection<SfCarouselItem> ProfilePhotos
    {
        get { return _profilePhotos; }
        set { _profilePhotos = value; OnPropertyChanged(nameof(ProfilePhotos)); }
    }

    [RelayCommand]

    public void Close()
    {

    }
}
