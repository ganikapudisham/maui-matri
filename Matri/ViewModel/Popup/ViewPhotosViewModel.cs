using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.Model;
using Matri.Views.Popups;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Matri.ViewModel;

public delegate Task CloseHandler<T>(T result);

public partial class ViewPhotosViewModel : ObservableObject, IQueryAttributable
{
    private readonly IServiceManager _serviceManager;

    public event CloseHandler<ViewPhotos>? OnClose;

    public ViewPhotosViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;

        CloseCommand = new RelayCommand<ViewPhotos>(async tt =>
        {
            if (OnClose is not null)
                await OnClose.Invoke(tt);
        });
    }

    public ICommand CloseCommand { get; }

    private ObservableCollection<ImageSource> profilePhotos = new();
    public ObservableCollection<ImageSource> ProfilePhotos
    {
        get => profilePhotos;
        set => SetProperty(ref profilePhotos, value);
    }

    // 🔑 This is where Shell parameters arrive
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(ProfileDetailsInput), out var param) && param is ProfileDetailsInput details)
        {
            await LoadPhotos(details);
        }
    }

    private async Task LoadPhotos(ProfileDetailsInput input)
    {
        var profileDetails = await _serviceManager.GetProfileById(input.LoggedInId, input.TargetProfileId);

        var tempPhotos = new ObservableCollection<ImageSource>();
        foreach (var pt in profileDetails.Photos)
        {
            tempPhotos.Add(ImageSource.FromUri(new Uri(pt.Name)));
        }

        ProfilePhotos = tempPhotos;
    }
}
