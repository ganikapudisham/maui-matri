using Matri.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;
using System.Windows.Input;
using Matri.Model;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Rotator;

namespace Matri.ViewModel;

public delegate Task CloseHandler<T>(T result);

public partial class ViewPhotosViewModel : ObservableObject
{
    IServiceManager _serviceManager;

    public event CloseHandler<ViewPhotos> OnClose;

    public ViewPhotosViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;

        CloseCommand = new RelayCommand<ViewPhotos>(async tt => await OnClose?.Invoke(tt));
    }

    public ICommand CloseCommand { get; set; }

    public async Task LoadPhotos(IDictionary<string, object> query)
    {
        var queryParam = query[nameof(ProfileDetailsInput)] as ProfileDetailsInput;
        var targetId = queryParam.TargetProfileId;
        var sourceId = queryParam.LoggedInId;

        var profileDetails = await _serviceManager.GetProfileById(sourceId, targetId);

        foreach (var pt in profileDetails.Photos)
        {
            var imageSource = ImageSource.FromUri(new Uri(pt.Name));
            tempPhotos.Add(imageSource);
        }

        ProfilePhotos = tempPhotos;
    }

    private ObservableCollection<ImageSource> tempPhotos = new ObservableCollection<ImageSource>();

    private ObservableCollection<ImageSource> profilePhotos = new ObservableCollection<ImageSource>();
    public ObservableCollection<ImageSource> ProfilePhotos
    {
        get { return profilePhotos; }
        set { profilePhotos = value; OnPropertyChanged(nameof(ProfilePhotos)); }
    }
}
