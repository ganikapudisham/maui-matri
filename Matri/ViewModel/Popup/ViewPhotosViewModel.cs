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

public delegate Task CloseHandler<T>(T result);

public partial class ViewPhotosViewModel : ObservableObject
{
    IServiceManager _serviceManager;

    private ObservableCollection<SfCarouselItem> _profilePhotos = new ObservableCollection<SfCarouselItem>();

    public event CloseHandler<ViewPhotos> OnClose;

    public ViewPhotosViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;

        CloseCommand = new RelayCommand<ViewPhotos>(async tt => await OnClose?.Invoke(tt));
    }

    public ICommand CloseCommand { get; set; }

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
}
