using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;
using AdminMatri.FontsAwesome;
using Matri.Abstract;
using Matri.Model;

namespace AdminMatri.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    IServiceManager _serviceManager;
    ISharedService _sharedService;
    public LoginViewModel(IServiceManager serviceManager, ISharedService sharedService)
    {
        _serviceManager = serviceManager;
        _sharedService = sharedService;
    }
#if DEBUG
    [ObservableProperty]
    public string eMobile = "admin";

    [ObservableProperty]
    public string ePassword = "1234";
#else
    [ObservableProperty]
    public string eMobile;

    [ObservableProperty]
    public string ePassword;

#endif

    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public bool isHiddenPassword = true;

    [ObservableProperty]
    public bool newVersionPromptVisibility = false;

    [ObservableProperty]
    public string newVersionAvailableMessage;

    [ObservableProperty]
    public string currentAppVersion;

    [ObservableProperty]
    public string bgColor = "White";

    [ObservableProperty]
    public string customerCareNumber = "";

    [ObservableProperty]
    public string passwordVisibilityIcon = FontAwesomeIcons.EyeSlash;

    [RelayCommand]
    public async Task Login()
    {
        IsBusy = true;
        var session = await _serviceManager.AdminLoginAsync(EMobile, EPassword);

        await SecureStorage.SetAsync("Token", session.SessionToken.ToString());

        var sessionToken = await SecureStorage.GetAsync("Token");
        var user = await _serviceManager.GetUserData(sessionToken);
        var masterData = await _serviceManager.GetMasterData(sessionToken);

        _sharedService.Add<Profile>("LoggedInUser", user);
        _sharedService.Add<MDD>("MasterData", masterData);

        IsBusy = false;
        await Shell.Current.GoToAsync("//leads");
    }
}
